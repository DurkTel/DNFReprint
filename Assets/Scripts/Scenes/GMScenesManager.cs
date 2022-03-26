using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GMScenesManager : SingletonMono<GMScenesManager>
{
    private static Transform m_transform;

    private Dictionary<int, GMScene> m_allScenes = new Dictionary<int, GMScene>();
    public static void Initialize()
    {
        m_transform = new GameObject("GMScenesManager").transform;
        m_transform.gameObject.AddComponent<GMScenesManager>();
        DontDestroyOnLoad(m_transform.gameObject);

    }

    /// <summary>
    /// 切换场景 同步
    /// </summary>
    /// <param name="name"></param>
    public void LoadScene(string name, LoadSceneMode loadSceneMode = LoadSceneMode.Additive)
    {
        //场景同步加载
        SceneManager.LoadScene(name, loadSceneMode);
    }

    /// <summary>
    /// 异步加载
    /// </summary>
    /// <param name="name"></param>
    public AsyncOperation LoadSceneAsyn(string name, LoadSceneMode loadSceneMode = LoadSceneMode.Additive)
    {
        GMScene scene = new GMScene();
        AsyncOperation ao = SceneManager.LoadSceneAsync(name, loadSceneMode);
        return ao;
    }

}
