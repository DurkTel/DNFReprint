using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class AssetManager : MonoBehaviour
{
    private static AssetManager m_instance;

    public static AssetManager Instance { get { return m_instance; } }

    private static bool m_useABLoadMode;
    public static bool UseABLoadMode { get { return m_useABLoadMode; } }

    private DictionaryEx<string, AssetLoader> m_assetLoaders = new DictionaryEx<string, AssetLoader>();

    private List<string> m_completeList = new List<string>();
    /// <summary>
    /// 主包
    /// </summary>
    private AssetBundle m_mainAB;
    /// <summary>
    /// 主包依赖信息
    /// </summary>
    private AssetBundleManifest m_manifest;
    /// <summary>
    /// ab包资源清单
    /// </summary>
    private AssetManifest_Bundle m_fileManifest;
    /// <summary>
    /// 所有加载过的ab包
    /// </summary>
    private Dictionary<string, AssetBundle> m_allAB = new Dictionary<string, AssetBundle>();

    public static void Initialize(bool useAB)
    {
        m_useABLoadMode = useAB;
        GameObject gameObject = new GameObject("AssetManager");
        m_instance = gameObject.AddComponent<AssetManager>();
    }

    public AssetManifest_Bundle GetAssetManifest_Bundle()
    { 
        if(m_fileManifest == null)
        {
            AssetBundle file = AssetBundle.LoadFromFile(Path.Combine(AssetDefine.localDataPath, "filemanifest"));
            m_allAB.Add("ABManifest.asset", file);
            m_fileManifest = file.LoadAsset<AssetManifest_Bundle>("ABManifest.asset");
        }

        return m_fileManifest;
    }

    public AssetBundle TryAddAssetBundle(string abName)
    {
        if(!m_allAB.ContainsKey(abName))
        {
            AssetBundle ab = AssetBundle.LoadFromFile(Path.Combine(AssetDefine.localDataPath, abName));
            AddAssetBundle(abName, ab);
        }

        return m_allAB[abName];
    }

    public void AddAssetBundle(string abName, AssetBundle bundle)
    {
        if (!m_allAB.ContainsKey(abName))
            m_allAB.Add(abName, bundle);
    }

    public bool RemoveAssetBundle(string abName)
    {
        if (m_allAB.ContainsKey(abName))
        { 
            m_allAB.Remove(abName);
            return true;
        }

        return false;
    }

    public bool TryGetAssetBundle(string abName, out AssetBundle bundle)
    {
        return m_allAB.TryGetValue(abName, out bundle);
    }

    /// <summary>
    /// 加载依赖
    /// </summary>
    public void LoadDependencies(string abName)
    {
        if (m_mainAB == null)
        {
            m_mainAB = AssetBundle.LoadFromFile(Path.Combine(AssetDefine.localDataPath, "StreamingAssets"));
            m_manifest = m_mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }

        foreach (string dependPath in m_manifest.GetDirectDependencies(abName))
        {
            if (!m_allAB.ContainsKey(dependPath))
            {
                AssetBundle ab = AssetBundle.LoadFromFile(Path.Combine(AssetDefine.localDataPath, dependPath));
                Debug.Assert(ab != null);
                m_allAB.Add(dependPath, ab);
            }
        }

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

            if (m_useABLoadMode)
                loader = new BundleAssetLoader(assetName.ToLower(), type, true);
            else
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
        AssetLoader loader;

        if (!m_assetLoaders.TryGetValue(assetName, out loader))
        {

            if (m_useABLoadMode)
                loader = new BundleAssetLoader(assetName.ToLower(), typeof(T), false);
            else
                loader = new EditorAssetLoader(assetName, typeof(T));

            loader.Update();
            m_assetLoaders.Add(assetName, loader);
        }

        return loader.rawObject as T;
    }

    /// <summary>
    /// 直接通过AB包同步加载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetName"></param>
    /// <param name="abName"></param>
    /// <returns></returns>
    public T LoadAsset<T>(string assetName, string abName) where T : UnityEngine.Object
    {
        LoadDependencies(abName);
        TryAddAssetBundle(abName); 
        return m_allAB[abName].LoadAsset<T>(assetName);
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
