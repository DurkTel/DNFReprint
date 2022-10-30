using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AssetManager : MonoBehaviour
{
    private static AssetManager m_instance;

    public static AssetManager Instance { get { return m_instance; } }  

    private DictionaryEx<string, AssetLoader> m_assetLoaders = new DictionaryEx<string, AssetLoader>();

    private List<string> m_completeList = new List<string>();

    public static void Initialize()
    {
        GameObject gameObject = new GameObject("AssetManager");
        m_instance = gameObject.AddComponent<AssetManager>();
    }

    /// <summary>
    /// 异步加载
    /// </summary>
    /// <param name="assetName"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public AssetLoader LoadAssetAsync(string assetName, Type type)
    {
        AssetLoader loader;

        if (!m_assetLoaders.TryGetValue(assetName, out loader))
        { 
            loader = new EditorAssetLoader(assetName, type);
            m_assetLoaders.Add(assetName, loader);
        }


        return loader;
    }

    /// <summary>
    /// 同步加载
    /// </summary>
    /// <param name="assetName"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public T LoadAsset<T>(string assetName) where T : UnityEngine.Object
    {
        AssetLoader loader = LoadAssetAsync(assetName, typeof(T));

        loader.Update();
        return loader.rawObject as T;
    }

    /// <summary>
    /// 移除加载器
    /// </summary>
    /// <param name="assetName"></param>
    public void RemoveAssetLoader(string assetName)
    {
        if (m_assetLoaders.TryGetValue(assetName, out AssetLoader loader))
        {
            loader.Dispose();
            m_assetLoaders.Remove(assetName);
        }
    }


    private void Update()
    {
        if (m_assetLoaders.Count <= 0) return;

        AssetLoader loader;

        for (int i = 0; i < m_assetLoaders.keyList.Count; i++)
        {
            string name = m_assetLoaders.keyList[i];
            loader = m_assetLoaders[name];
            loader.onProgress?.Invoke(loader.progress);

            loader.Update();

            if (loader.isDone)
            {
                m_completeList.Add(name);
            }
        }


        if (m_completeList.Count <= 0) return;

        foreach (string name in m_completeList)
        {
            if (m_assetLoaders.TryGetValue(name, out loader))
            {
                loader.onComplete?.Invoke(loader);
                m_assetLoaders.Remove(name);
            }
        }

        m_completeList.Clear();
    }
}
