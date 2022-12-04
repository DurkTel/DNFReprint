using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class AssetUtility
{

    public static TextAsset LoadLuaFile(string path)
    {
        return AssetManager.Instance.LoadAsset<TextAsset>(path, "lua");
    }

    /// <summary>
    /// 异步加载
    /// </summary>
    /// <param name="assetName"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static AssetLoader LoadAssetAsync(string assetName, System.Type type)
    {
        return AssetManager.Instance.LoadAssetAsync(assetName, type);
    }

    /// <summary>
    /// 异步加载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetName"></param>
    /// <returns></returns>
    public static AssetLoader LoadAssetAsync<T>(string assetName)
    { 
        return LoadAssetAsync(assetName, typeof(T));
    }

    /// <summary>
    /// 同步加载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetName"></param>
    /// <returns></returns>
    public static T LoadAsset<T>(string assetName) where T : UnityEngine.Object
    { 
        return AssetManager.Instance.LoadAsset<T>(assetName);
    }

    /// <summary>
    /// 停止加载
    /// </summary>
    /// <param name="asstName"></param>
    public static void StopLoadingAsset(string asstName)
    { 
        AssetManager.Instance.RemoveAssetLoader(asstName);
    }

    /// <summary>
    /// 获取AB包资源清单
    /// </summary>
    /// <returns></returns>
    public static AssetManifest_Bundle GetAssetManifest_Bundle()
    {
        return AssetManager.Instance.GetAssetManifest_Bundle();
    }

    /// <summary>
    /// 添加已加载的AB包
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="bundle"></param>
    public static void AddAssetBundle(string abName, AssetBundle bundle)
    {
        AssetManager.Instance.AddAssetBundle(abName, bundle);
    }

    /// <summary>
    /// 获取AB包
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="bundle"></param>
    /// <returns></returns>
    public static bool TryGetAssetBundle(string abName, out AssetBundle bundle)
    {
        return AssetManager.Instance.TryGetAssetBundle(abName, out bundle);
    }

    public static void LoadDependencies(List<string> abNames)
    {
        AssetManager.Instance.LoadDependencies(abNames);
    }

    /// <summary>
    /// 清除已加载的AB包
    /// </summary>
    /// <param name="abName"></param>
    public static void RemoveAssetBundle(string abName)
    {
        AssetManager.Instance.RemoveAssetBundle(abName);
    }

    /// <summary>
    /// 销毁/释放资源
    /// </summary>
    /// <param name="obj"></param>
    public static void Destroy(UnityEngine.Object obj)
    {
        AssetCache.Destroy(obj);
    }

    public static string GetMD5(string file)
    {
        try
        {
            FileStream fs = new FileStream(file, FileMode.Open);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(fs);
            fs.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }

            return sb.ToString();
        }
        catch (System.Exception ex)
        {
            throw new System.Exception("md5file() fail, error:" + ex.Message);
        }
    }
}
