using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using AI;
using System;

public class GMScenesManager : SingletonMono<GMScenesManager>
{
    private static Transform m_transform;

    private Vector3 initPos = new Vector3(9999, 9999, 9999);

    private Vector3 actPos = new Vector3(0, 0, 50);

    private Dictionary<int, GMScene> m_allScenes = new Dictionary<int, GMScene>();

    private List<int> m_destroyList = new List<int>();

    private int m_frameCount;

    private GMScene m_curScene;
    public GMScene curScene { get { return m_curScene; } }

    private GMScene m_lastScene;
    public GMScene lastScene { get { return m_lastScene; } }

    private Navigation2D m_navigation2D;
    public Navigation2D navigation2D { get { return m_navigation2D; } }
    public static Action<int> on_LoadEvent { get; set; }
    public static Action<int> on_CompleteEvent { get; set; }
    public static Action<int> on_ActivateEvent { get; set; }
    public static Action<int> on_UnActivateEvent { get; set; }
    public static Action<int> on_ReleaseEvent { get; set; }

    public static void Initialize()
    {
        m_transform = new GameObject("GMScenesManager").transform;
        m_transform.gameObject.AddComponent<GMScenesManager>();
        DontDestroyOnLoad(m_transform.gameObject);

    }

    /// <summary>
    /// 同步加载场景
    /// </summary>
    /// <param name="mapId"></param>
    public GMScene LoadScene(int mapId, string path, UnityAction callBack = null)
    {
        on_LoadEvent?.Invoke(mapId);
        GMScene scene = Pool<GMScene>.Get();
        GameObject go = AssetLoader.Load<GameObject>(path);
        if (go == null)
        {
            Debug.LogError("场景加载错误，资源路径没有该资源");
            return null;
        }
        GameObject sceneObj = Instantiate(go);
        if (!m_allScenes.ContainsKey(mapId))
            m_allScenes.Add(mapId, scene);
        //加载完先放一边
        sceneObj.transform.position = initPos;
        scene.gameObject = sceneObj;
        on_CompleteEvent?.Invoke(mapId);
        callBack?.Invoke();
        return scene;
    }

    public GMScene LoadSceneAsyn(int mapId, string path, UnityAction callBack = null)
    {
        on_LoadEvent?.Invoke(mapId);
        GMScene scene = Pool<GMScene>.Get();
        AssetLoader.LoadAsync<GameObject>(path, (p) =>
        {
            Debug.Assert(p != null, "场景加载错误，资源路径没有该资源");
            GameObject sceneObj = Instantiate(p);
            if (!m_allScenes.ContainsKey(mapId))
                m_allScenes.Add(mapId, scene);
            //加载完先放一边
            sceneObj.transform.position = initPos;
            scene.gameObject = sceneObj;
            on_CompleteEvent?.Invoke(mapId);
            callBack?.Invoke();
        });
        return scene;
    }

    public void SwitchScene(int mapId, string path)
    {
        if (m_allScenes.TryGetValue(mapId, out GMScene scene))
        {
            on_LoadEvent?.Invoke(mapId);
            if (m_curScene != null)
            { 
                m_curScene.Unactivation();
                m_curScene.transform.position = initPos;
            }

            m_lastScene = m_curScene;
            m_curScene = scene;
            m_curScene.Activate();
            m_curScene.transform.position = actPos;
            on_ActivateEvent?.Invoke(mapId);

            if (!m_curScene.gameObject.TryGetComponent<Navigation2D>(out m_navigation2D))
            {
                Debug.LogError("该地图没有寻路网格！" + mapId);
            }
        }
        else
        {
            LoadSceneAsyn(mapId, path, () => SwitchScene(mapId, path));
        }
    }


    private void Update()
    {
        if (++m_frameCount % 60 == 0 && m_allScenes.Count > 0)
        {
            foreach (var scene in m_allScenes)
            {
                if (m_curScene != scene.Value && Time.realtimeSinceStartup - scene.Value.releaseTime >= 600)
                {
                    m_destroyList.Add(scene.Key);
                    
                }
            }

            if (m_destroyList.Count > 0)
            {
                foreach (var key in m_destroyList)
                {
                    Destroy(m_allScenes[key].gameObject);
                    on_ReleaseEvent?.Invoke(key);
                    m_allScenes[key].Release();
                }
            }
        }
    }
}
