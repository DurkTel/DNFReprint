using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorAssetLoader : AssetLoader
{
    public EditorAssetLoader(string assetName) : base(assetName)
    {
    }

    //编辑模式下加载没有异步
    public EditorAssetLoader(string assetName, Type assetType) : base(assetName, assetType, false)
    {
    }

    public override void Dispose()
    {
        m_error = false;
        m_isDone = false;
        m_async = false;
        m_rawObject = null;
        m_assetName = null;
        m_assetType = null;
        onProgress = null;
        onComplete = null;
    }

    public override string GetAssetPath(string assetName)
    {
        return AssetManifest.GetAssetManifest().GetPath(assetName);
    }

    public override void Update()
    {
        if(m_isDone || m_error)
            return;

        if (!AssetCache.TryGetRawObject(m_assetName, out m_rawObject))
        {
            string path = GetAssetPath(m_assetName);
            if (path == "")
            {
                m_error = true;
                AssetUtility.StopLoadingAsset(m_assetName);
                return;
            }

            m_rawObject = AssetDatabase.LoadAssetAtPath(path, m_assetType);
            if (m_rawObject != null)
                AssetCache.AddRawObject(m_assetName, m_rawObject);
        }
        else
            m_rawObject = AssetCache.GetRawObject(m_assetName);


        m_isDone = m_rawObject != null;
    }
}
