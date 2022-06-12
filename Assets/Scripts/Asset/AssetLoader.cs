using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AssetLoader
{
    public enum LoadMode
    {
        Resources,
        AssetBundle
    }

    public static LoadMode loadMode;

    #region 同步加载
    /// <summary>
    /// 同步加载资源
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="name">资源名称</param>
    /// <param name="abName">ab包名称（如果当前是ab包模式）</param>
    /// <returns></returns>
    public static T Load<T>(string name, string abName = "") where T : Object
    {
        T asset = default;
        switch (loadMode)
        {
            case LoadMode.Resources:
                asset = LoadByResources<T>(name);
                break;
            case LoadMode.AssetBundle:
                string[] ab = name.Split(new[] { '/' }, 2);
                Debug.Assert(!string.IsNullOrEmpty(ab[0]), "当前为AB包加载模式，请输入ab包名");
                asset = LoadByAssetBundle<T>(ab[0], ab[1]);
                break;
        }

        return asset;
    }

    private static T LoadByResources<T>(string name) where T : Object
    {
        T res = Resources.Load<T>(name);
        return res;
    }

    private static T LoadByAssetBundle<T>(string abName, string name) where T : Object
    {
        T res = AssetBundleManager.Instance.Load<T>(abName, name);
        return res;
    }
    #endregion


    #region 异步加载
    /// <summary>
    /// 异步加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="abName"></param>
    /// <param name="callBack"></param>
    public static void LoadAsync<T>(string name, System.Action<T> callBack = null) where T : Object
    {
        switch (loadMode)
        {
            case LoadMode.Resources:
                LoadAsyncByResources(name, callBack);
                break;
            case LoadMode.AssetBundle:
                string[] ab = name.Split(new[] { '/' }, 2);
                Debug.Assert(!string.IsNullOrEmpty(ab[0]), "当前为AB包加载模式，请输入ab包名");
                LoadAsyncByAssetBundle(ab[0], ab[1], callBack);
                break;
        }
    }

    public static AsyncOperation LoadAsyncAO<T>(string name, string abName = "") where T : Object
    {
        switch (loadMode)
        {
            case LoadMode.Resources:
                ResourceRequest re = LoadAsyncByResources<T>(name);
                return re;
            case LoadMode.AssetBundle:
                string[] ab = name.Split(new[] { '/' }, 2);
                Debug.Assert(!string.IsNullOrEmpty(ab[0]), "当前为AB包加载模式，请输入ab包名");
                AssetBundleRequest ar = LoadAsyncByAssetBundle<T>(ab[0], ab[1]);
                return ar;
        }

        return default(AsyncOperation);
    }

    private static ResourceRequest LoadAsyncByResources<T>(string name) where T : Object
    {
        ResourceRequest re = Resources.LoadAsync<T>(name);

        return re;
    }

    private static void LoadAsyncByResources<T>(string name, System.Action<T> callBack) where T : Object
    {
        ResourceRequest re = Resources.LoadAsync<T>(name);

        re.completed += (p) =>
        {
            if (callBack != null)
                callBack(re.asset as T);
        };
    }

    private static AssetBundleRequest LoadAsyncByAssetBundle<T>(string abName, string assetName) where T : Object
    {
        return AssetBundleManager.Instance.LoadAsync<T>(abName, assetName);
    }

    private static void LoadAsyncByAssetBundle<T>(string abName, string assetName, System.Action<T> callBack = null) where T : Object
    {
        AssetBundleManager.Instance.LoadAsync(abName, assetName, callBack);
    }

    #endregion
}
