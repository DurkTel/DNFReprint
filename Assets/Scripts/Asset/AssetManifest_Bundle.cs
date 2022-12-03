using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetManifest_Bundle : ScriptableObject, ISerializationCallbackReceiver
{
    public const string path = "Assets/Art/ABManifest.asset";

    [System.Serializable]
    public class AssetInfo
    {
        public string assetName;
        public string bundleName;
        public string assetPath;
        public AssetInfo(string assetName, string assetPath, string bundleName)
        {
            this.assetName = assetName;
            this.assetPath = assetPath;
            this.bundleName = bundleName;
        }
    }

    public List<AssetInfo> assetList = new List<AssetInfo>(5000);

    public Dictionary<string, AssetInfo> assetMap = new Dictionary<string, AssetInfo>(5000);
#if UNITY_EDITOR
    public static void RefreshAssetsBundleManifest()
    {
        AssetManifest_Bundle assetManifest = GetAssetManifest();
        assetManifest.Clear();

        AssetBundle m_mainAB = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "StreamingAssets"));
        AssetBundleManifest m_manifest = m_mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        string[] abNames = m_manifest.GetAllAssetBundles();
        foreach (string abName in abNames)
        {
            if (abName == "lua") continue; //lua�ļ�����Ҫ���嵥

            string abPath = Path.Combine(Application.streamingAssetsPath, abName);
            AssetBundle ab = AssetBundle.LoadFromFile(abPath);
            foreach (string assetName in ab.GetAllAssetNames())
            {
                string name = Path.GetFileName(assetName);
                assetManifest.Add(name, assetName, ab.name);
            }
        }

        AssetBundle.UnloadAllAssetBundles(true);

        EditorUtility.SetDirty(assetManifest);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        AssetImporter importer = AssetImporter.GetAtPath(path.Substring(path.IndexOf("Asset")));
        if (importer != null)
        {
            importer.assetBundleName = "filemanifest";
        }
        AssetDatabase.Refresh();

        Debug.Log("����AB��Դ�嵥���");
    }

    public static AssetManifest_Bundle GetAssetManifest()
    {
        AssetManifest_Bundle assetManifest = AssetDatabase.LoadAssetAtPath<AssetManifest_Bundle>(path);

        if (assetManifest == null)
        {
            assetManifest = ScriptableObject.CreateInstance<AssetManifest_Bundle>();
            AssetDatabase.CreateAsset(assetManifest, path);
        }

        return assetManifest;
    }

#endif
    public void Add(string assetName, string assetPath, string bundleName)
    {
        if (assetMap.ContainsKey(assetName))
        {
            assetMap[assetName].assetPath = assetPath;
            assetMap[assetName].bundleName = bundleName;

        }
        else
            assetMap.Add(assetName, new AssetInfo(assetName, assetPath, bundleName));
    }

    public bool Contains(string assetName)
    {
        return assetMap.ContainsKey(assetName);
    }

    public string GetPath(string assetName)
    {
        if (assetMap.ContainsKey(assetName))
            return assetMap[assetName].assetPath;

        Debug.LogWarning("��Դ�嵥��û����Ϊ��" + assetName + "����Դ���������Դ�嵥������Դ����");
        return string.Empty;
    }

    public string GetBundleName(string assetName)
    {
        if (assetMap.ContainsKey(assetName))
            return assetMap[assetName].bundleName;

        Debug.LogWarning("��Դ�嵥��û����Ϊ��" + assetName + "����Դ���������Դ�嵥������Դ����");
        return string.Empty;
    }

    public void Clear()
    {
        assetMap.Clear();
    }

    public void OnAfterDeserialize()
    {
        foreach (var item in assetList)
            if (!this.assetMap.ContainsKey(item.assetName))
                this.assetMap.Add(item.assetName, item);
    }

    public void OnBeforeSerialize()
    {
        assetList.Clear();
        foreach (var item in this.assetMap)
            assetList.Add(item.Value);
    }
}
