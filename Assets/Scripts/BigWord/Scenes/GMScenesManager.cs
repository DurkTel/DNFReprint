﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using cfg.db;

public class GMScenesManager : SingletonMono<GMScenesManager>
{
    private static Transform m_transform;

    private Vector3 initPos = new Vector3(9999, 9999, 9999);

    private Dictionary<int, GMScene> m_allScenes = new Dictionary<int, GMScene>();

    private List<int> m_destroyList = new List<int>();

    private int m_frameCount;

    private GMScene m_curScene;
    public GMScene curScene { get { return m_curScene; } }

    private GMScene m_lastScene;
    public GMScene lastScene { get { return m_lastScene; } }
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
    public void LoadScene(int mapId)
    {
        GMScene scene = Pool<GMScene>.Get();
        MapCfg map = MDefine.tables.TbMap.Get(mapId);
        GameObject go = AssetLoader.Load<GameObject>(map.AssetName);
        if (go == null)
        {
            Debug.LogError("场景加载错误，资源路径没有该资源");
            return;
        }
        GameObject sceneObj = Instantiate(go);
        scene.SetData(sceneObj, map);
        if (!m_allScenes.ContainsKey(mapId))
            m_allScenes.Add(mapId, scene);
        //加载完先放一边
        sceneObj.transform.position = initPos;
    }

    public void LoadSceneAsyn(int mapId, UnityAction callBack = null)
    {
        GMScene scene = Pool<GMScene>.Get();
        MapCfg map = MDefine.tables.TbMap.Get(mapId);
        ResourceRequest re = AssetLoader.LoadAsync<GameObject>(map.AssetName);
        if (re == null)
        {
            Debug.LogError("场景加载错误，资源路径没有该资源");
            return;
        }
        re.completed += (p) =>
        {
            GameObject sceneObj = Instantiate(re.asset as GameObject);
            scene.SetData(sceneObj, map);
            if (!m_allScenes.ContainsKey(mapId))
                m_allScenes.Add(mapId, scene);
            //加载完先放一边
            sceneObj.transform.position = initPos;

            callBack?.Invoke();
        };
    }

    public void SwitchScene(int mapId, Vector3 pos = default(Vector3))
    {
        if (m_allScenes.TryGetValue(mapId, out GMScene scene))
        {
            if (m_curScene != null)
                m_curScene.Inactivation();

            m_lastScene = m_curScene;
            m_curScene = scene;
            m_curScene.Activate(pos);
        }
        else
        {
            LoadSceneAsyn(mapId, () => SwitchScene(mapId, pos));
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
                    m_allScenes[key].Release();
                }
            }
        }
    }
}
