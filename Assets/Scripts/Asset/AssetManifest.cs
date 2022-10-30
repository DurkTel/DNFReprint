using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetManifest : ScriptableObject, ISerializationCallbackReceiver
{
    public const string path = "Assets/Plugins/AssetManifest.asset";
    [System.Serializable]
    public class AssetInfo
    {
        public string assetName;
        public string assetPath;
        public AssetInfo(string assetName, string assetPath)
        {
            this.assetName = assetName;
            this.assetPath = assetPath;
        }
    }
    public List<AssetInfo> assetList = new List<AssetInfo>();

    public Dictionary<string, string> assetMap = new Dictionary<string, string>();

    public static AssetManifest GetAssetManifest()
    {
        AssetManifest assetManifest = AssetDatabase.LoadAssetAtPath<AssetManifest>(path);

        if (assetManifest == null)
        { 
            assetManifest = ScriptableObject.CreateInstance<AssetManifest>();
            AssetDatabase.CreateAsset(assetManifest, path);
        }

        return assetManifest;
    }

    public void Add(string assetPath)
    {
        string assetName = Path.GetFileName(assetPath);

        if (assetMap.ContainsKey(assetName))
            assetMap[assetName] = assetPath;
        else
            assetMap.Add(assetName, assetPath);
    }

    public bool Contains(string assetName)
    { 
        return assetMap.ContainsKey(assetName);
    }

    public string GetPath(string assetName)
    {
        if (assetMap.ContainsKey(assetName))
            return assetMap[assetName];

        Debug.LogWarning("资源清单中没有名为：" + assetName + "的资源，请更新资源清单或检查资源名称");
        return string.Empty;
    }

    public void Clear()
    {
        assetMap.Clear();
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Refresh AssetsManifest")]
#endif
    public static void RefreshAssetsManifest()
    {
        AssetManifest assetManifest = GetAssetManifest();
        assetManifest.Clear();

        string[] allfile = Directory.GetFiles(Application.dataPath, "*", SearchOption.AllDirectories);
        int spos = Application.dataPath.Length - 6;
        foreach (var item in allfile)
        {
            string path = item.Substring(spos).Replace("\\", "/");
            if (path.StartsWith("Assets"))
            {
                string ex = Path.GetExtension(path);
                if (ex != ".meta" && ex != ".cs")
                { 
                    assetManifest.Add(path);
                }
            }
        }
        EditorUtility.SetDirty(assetManifest);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("更新资源清单完成");
    }

    public void OnBeforeSerialize()
    {
        assetList.Clear();
        foreach (var item in this.assetMap)
            assetList.Add(new AssetInfo(item.Key, item.Value));
    }

    public void OnAfterDeserialize()
    {
        foreach (var item in assetList)
            if (!this.assetMap.ContainsKey(item.assetName))
                this.assetMap.Add(item.assetName, item.assetPath);
    }
}
