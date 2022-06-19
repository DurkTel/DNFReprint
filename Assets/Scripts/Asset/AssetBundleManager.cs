using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AssetBundleManager : SingletonBase<AssetBundleManager>
{
    /// <summary>
    /// 包路径
    /// </summary>
    private string m_abPathUrl = AssetDefine.localDataPath;
    /// <summary>
    /// 主包
    /// </summary>
    private AssetBundle m_mainAB;
    /// <summary>
    /// 主包依赖信息
    /// </summary>
    private AssetBundleManifest m_manifest;
    /// <summary>
    /// 所有加载过的ab包
    /// </summary>
    private Dictionary<string, AssetBundle> m_allAB = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// 加载依赖
    /// </summary>
    private void LoadDependencies(string abName)
    {
        if (m_mainAB == null)
        {
            m_mainAB = AssetBundle.LoadFromFile(Path.Combine(m_abPathUrl, "StreamingAssets"));
            m_manifest = m_mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }

        foreach (string dependPath in m_manifest.GetDirectDependencies(abName))
        {
            if (!m_allAB.ContainsKey(dependPath))
            {
                AssetBundle ab = AssetBundle.LoadFromFile(Path.Combine(m_abPathUrl, dependPath));
                Debug.Assert(ab != null);
                m_allAB.Add(dependPath, ab);
            }
        }

    }

    /// <summary>
    /// 同步加载资源
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="abName">ab包名</param>
    /// <param name="assetName">资源名</param>
    /// <returns></returns>
    public T Load<T>(string abName, string assetName) where T : Object
    {
        LoadDependencies(abName);
        AssetBundle ab;
        if (!m_allAB.TryGetValue(abName, out ab))
        {
            ab = AssetBundle.LoadFromFile(Path.Combine(m_abPathUrl, abName));
            m_allAB.Add(abName, ab);
        }

        T obj = ab.LoadAsset<T>(assetName);

        return obj;
    }

    public AssetBundleRequest LoadAsync<T>(string abName, string assetName) where T : Object
    {
        LoadDependencies(abName);
        AssetBundle ab;
        if (!m_allAB.TryGetValue(abName, out ab))
        {
            ab = AssetBundle.LoadFromFile(Path.Combine(m_abPathUrl, abName));
            m_allAB.Add(abName, ab);
        }

        return ab.LoadAssetAsync<T>(assetName);
    }

    /// <summary>
    /// 异步加载
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="abName">ab包名</param>
    /// <param name="assetName">资源名</param>
    /// <param name="callBack">回调事件</param>
    public void LoadAsync<T>(string abName, string assetName, System.Action<T> callBack = null) where T : Object
    {
        LoadDependencies(abName);
        TimerManager.AddCoroutine(LoadAsyncEnumer(abName, assetName, callBack));
    }

    private IEnumerator LoadAsyncEnumer<T>(string abName, string assetName, System.Action<T> callBack) where T : Object
    {
        AssetBundle ab;
        if (!m_allAB.TryGetValue(abName, out ab))
        {
            AssetBundleCreateRequest cr = AssetBundle.LoadFromFileAsync(Path.Combine(m_abPathUrl, abName));

            yield return cr;
            ab = cr.assetBundle;
            m_allAB.Add(abName, ab);
        }

        AssetBundleRequest re = ab.LoadAssetAsync<T>(assetName);

        yield return re;

        if (callBack != null)
            callBack(re.asset as T);
    }

    public void Unload(string abName, bool unloadAllLoadedObjects = false)
    {
        if (m_allAB.TryGetValue(abName, out AssetBundle bundle))
        {
            bundle.Unload(unloadAllLoadedObjects);
        }
    }

    public void UnloadAll(bool unloadAllLoadedObjects = false)
    {
        AssetBundle.UnloadAllAssetBundles(unloadAllLoadedObjects);
    }
}
