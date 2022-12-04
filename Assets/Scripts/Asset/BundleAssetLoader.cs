using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using XLua;

public class BundleAssetLoader : AssetLoader
{
    public BundleAssetLoader(string assetName) : base(assetName)
    {
    }

    public BundleAssetLoader(string assetName, System.Type assetType, bool async) : base(assetName, assetType, async)
    {
        this.m_assetName = assetName;
        this.m_assetType = assetType;
        this.m_async = async;
    }

    private AssetBundleRequest m_bundleRequest;

    private string m_bundleName;

    public override void Dispose()
    {
        m_error = false;
        m_isDone = false;
        m_async = false;
        m_rawObject = null;
        m_assetName = null;
        m_bundleName = null;
        m_assetType = null;
        onProgress = null;
        onComplete = null;
        m_bundleRequest = null;
    }

    public override string GetAssetPath(string assetName)
    {
        return AssetUtility.GetAssetManifest_Bundle().GetPath(assetName);
    }

    public string GetBundleName(string assetName)
    {
        return AssetUtility.GetAssetManifest_Bundle().GetBundleName(assetName);
    }

    public List<string> GetDependsName(string assetName)
    {
        return AssetUtility.GetAssetManifest_Bundle().GetDependsName(assetName);
    }

    public override void Update()
    {
        if (m_isDone || m_error)
            return;

        if (!AssetCache.TryGetRawObject(m_assetName, out m_rawObject))
        {
            m_bundleName ??= GetBundleName(m_assetName);
            if (string.IsNullOrEmpty(m_bundleName))
            {
                m_error = true;
                AssetUtility.StopLoadingAsset(m_assetName);
                return;
            }

            if (m_async)
            {
                m_bundleRequest ??= LoadAsync(m_bundleName, m_assetName, m_assetType);

                if (m_bundleRequest.isDone)
                    m_rawObject = m_bundleRequest.asset;
            }
            else
            {
                m_rawObject = Load(m_bundleName, m_assetName, m_assetType);
            }

            if (m_rawObject != null)
                AssetCache.AddRawObject(m_assetName, m_rawObject);
        }
        else
            m_rawObject = AssetCache.GetRawObject(m_assetName);


        m_isDone = m_rawObject != null;
    }

    private UnityEngine.Object Load(string abName, string assetName, Type type)
    {
        AssetUtility.LoadDependencies(GetDependsName(abName));
        AssetBundle ab;
        if (!AssetUtility.TryGetAssetBundle(abName, out ab))
        {
            ab = AssetBundle.LoadFromFile(Path.Combine(AssetDefine.localDataPath, abName));
            AssetUtility.AddAssetBundle(abName, ab);
        }

        UnityEngine.Object obj = ab.LoadAsset(assetName, type);

        return obj;
    }

    private AssetBundleRequest LoadAsync(string abName, string assetName, Type type)
    {
        AssetUtility.LoadDependencies(GetDependsName(abName));
        AssetBundle ab;
        if (!AssetUtility.TryGetAssetBundle(abName, out ab))
        {
            ab = AssetBundle.LoadFromFile(Path.Combine(AssetDefine.localDataPath, abName));
            AssetUtility.AddAssetBundle(abName, ab);
        }

        return ab.LoadAssetAsync(assetName, type);
    }
}
