using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class AssetCache
{
    private static Dictionary<string, Object> m_rawAssetMap = new Dictionary<string, Object>();

    private static Dictionary<Object, int> m_renferenceCounts = new Dictionary<Object, int>();


    /// <summary>
    /// 缓存资源源对象
    /// </summary>
    /// <param name="rawObject"></param>
    public static void AddRawObject(string assetName, Object rawObject)
    {
        if (m_rawAssetMap.ContainsKey(assetName))
            return;

        m_rawAssetMap.Add(assetName, rawObject);
        AddRenferenceCount(rawObject);
    }

    /// <summary>
    /// 删除缓存资源
    /// </summary>
    /// <param name="assetName"></param>
    /// <returns></returns>
    public static bool RemoveRawObject(string assetName)
    {
        if (m_rawAssetMap.ContainsKey(assetName))
        { 
            m_rawAssetMap.Remove(assetName);
            return true;
        }

        return false;
    }

    /// <summary>
    /// 获取缓存资源
    /// </summary>
    /// <param name="assetName"></param>
    /// <returns></returns>
    public static Object GetRawObject(string assetName)
    {
        Object rawObject;
        if (!m_rawAssetMap.TryGetValue(assetName, out rawObject))
            return null;

        AddRenferenceCount(rawObject);
        return rawObject;
    }

    /// <summary>
    /// 尝试获取缓存资源
    /// </summary>
    /// <param name="assetName"></param>
    /// <param name="rawObject"></param>
    /// <returns></returns>
    public static bool TryGetRawObject(string assetName, out Object rawObject)
    {
        rawObject = GetRawObject(assetName);

        return rawObject != null;
    }

    public static void Destroy(Object obj)
    {
        RemoveRenferenceCount(obj);

        if (m_renferenceCounts.ContainsKey(obj) && m_renferenceCounts[obj] > 0)
            return;

        if (obj is GameObject)
            Object.Destroy(obj);
        else
            Resources.UnloadAsset(obj);

    }

    private static void AddRenferenceCount(Object rawObject)
    {
        if (!m_renferenceCounts.ContainsKey(rawObject))
            m_renferenceCounts[rawObject] = 0;
        m_renferenceCounts[(rawObject)]++;
    }

    private static void RemoveRenferenceCount(Object rawObject)
    {
        if (m_renferenceCounts.ContainsKey(rawObject))
        {
            if (--m_renferenceCounts[rawObject] <= 0)
            {
                m_renferenceCounts.Remove(rawObject);
            }
        }

    }
}
