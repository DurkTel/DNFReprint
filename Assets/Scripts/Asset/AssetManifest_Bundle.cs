using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AssetManifest_Bundle : ScriptableObject, ISerializationCallbackReceiver
{
    public const string path = "Assets/Art/ABManifest.asset";

    [System.Serializable]
    public class AssetInfo
    {
        public string assetName;
        public string assetPath;
        public string bundleName;
        public List<string> dependencieBundleNames;
        public AssetInfo(string assetName, string assetPath, string bundleName, List<string> dependencieBundleNames)
        {
            this.assetName = assetName;
            this.assetPath = assetPath;
            this.bundleName = bundleName;
            this.dependencieBundleNames = dependencieBundleNames;
        }
    }

    public List<AssetInfo> assetList = new List<AssetInfo>(5000);

    public Dictionary<string, AssetInfo> assetMap = new Dictionary<string, AssetInfo>(5000);
#if UNITY_EDITOR
    public static void RefreshAssetsBundleManifest()
    {
        AssetManifest_Bundle assetManifest = GetAssetManifest();
        assetManifest.Clear();

        AssetBundle mainAB = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "StreamingAssets"));
        AssetBundleManifest manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        string[] abNames = manifest.GetAllAssetBundles();
        foreach (string abName in abNames)
        {
            if (abName == "lua") continue; //lua文件不需要在清单
            //记录依赖
            List<string> dependBundleNames = manifest.GetDirectDependencies(abName).ToList<string>();
            //记录资源映射
            string abPath = Path.Combine(Application.streamingAssetsPath, abName);
            AssetBundle ab = AssetBundle.LoadFromFile(abPath);
            foreach (string assetName in ab.GetAllAssetNames())
            {
                string name = Path.GetFileName(assetName);
                assetManifest.Add(name, assetName, ab.name, dependBundleNames);
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

        Debug.Log("更新AB资源清单完成");
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
    public void Add(string assetName, string assetPath, string bundleName, List<string> dependencieBundleNames)
    {
        if (assetMap.ContainsKey(assetName))
        {
            assetMap[assetName].assetPath = assetPath;
            assetMap[assetName].bundleName = bundleName;

        }
        else
            assetMap.Add(assetName, new AssetInfo(assetName, assetPath, bundleName, dependencieBundleNames));
    }

    public bool Contains(string assetName)
    {
        return assetMap.ContainsKey(assetName);
    }

    public string GetPath(string assetName)
    {
        if (assetMap.ContainsKey(assetName))
            return assetMap[assetName].assetPath;

        Debug.LogWarning("资源清单中没有名为：" + assetName + "的资源，请更新资源清单或检查资源名称");
        return string.Empty;
    }

    public string GetBundleName(string assetName)
    {
        if (assetMap.ContainsKey(assetName))
            return assetMap[assetName].bundleName;

        Debug.LogWarning("资源清单中没有名为：" + assetName + "的资源，请更新资源清单或检查资源名称");
        return string.Empty;
    }

    public List<string> GetDependsName(string assetName)
    {
        if (assetMap.ContainsKey(assetName))
            return assetMap[assetName].dependencieBundleNames;

        Debug.LogWarning("资源清单中没有名为：" + assetName + "的资源，请更新资源清单或检查资源名称");
        return default;
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
