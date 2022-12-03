using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class AssetBundleMeun
{

    [MenuItem("Assets/Build/Build AssetBundle")]
    private static void BuildAssetBundles()
    {
        string abPath = Application.streamingAssetsPath;
        if (!Directory.Exists(abPath))
        {
            Directory.CreateDirectory(abPath);
        }
        else
        {
            string[] allFiles = Directory.GetFiles(abPath);
            for (int i = 0; i < allFiles.Length; i++)
            {
                File.Delete(allFiles[i]);
            }
        }

        BuildLua();
        BuildAsset(AssetDefine.audioBuildPath, "audio");
        BuildAsset(AssetDefine.effectBuildPath, "effect");
        BuildAsset(AssetDefine.fontBuildPath, "font");
        BuildAsset(AssetDefine.guiBuildPath, "gui");
        BuildAsset(AssetDefine.modelBuildPath, "model");
        BuildAsset(AssetDefine.sceneBuildPath, "scene");
        BuildAsset(AssetDefine.scriptObjectBuildPath, "scriptobject");
        AssetDatabase.Refresh();

        BuildPipeline.BuildAssetBundles(abPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows);
        Directory.Delete(AssetDefine.luaBuildTemp, true);
        AssetDatabase.Refresh();

        //生成AB资源清单
        AssetManifest_Bundle.RefreshAssetsBundleManifest();
        BuildPipeline.BuildAssetBundles(abPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows);
        AssetDatabase.Refresh();
        Debug.Log("打包完成 ^^_");
    }

    #region 打包lua
    private static void BuildLua()
    {
        List<string> pathList = new List<string>();
        CopyAllLuaFile(AssetDefine.luaPath, AssetDefine.luaBuildTemp, ref pathList);
        AssetDatabase.Refresh();

        foreach (var path in pathList)
        {
            AssetImporter importer = AssetImporter.GetAtPath(path.Substring(path.IndexOf("Asset")));
            if (importer != null)
            {
                importer.assetBundleName = "lua";
            }

        }

    }

    private static void CopyAllLuaFile(string sourceFileName, string destFileName, ref List<string> pathList)
    {
        if (!Directory.Exists(destFileName))
            Directory.CreateDirectory(destFileName);
        else
        {
            foreach (var oldFile in Directory.GetFiles(destFileName, "*.txt"))
            {
                File.Delete(oldFile);
            }
        }

        foreach (var file in Directory.GetFiles(sourceFileName, "*.lua"))
        {
            string name = Path.GetFileName(file);
            string dest = Path.Combine(destFileName, name) + ".txt";
            pathList.Add(dest);
            File.Copy(file, dest);
        }

        foreach (var directory in Directory.GetDirectories(sourceFileName))
        {
            string dirName = Path.GetFileName(directory);
            string dest = Path.Combine(destFileName, dirName);
            CopyAllLuaFile(directory, dest, ref pathList);
        }
    }
    #endregion

    #region 打包资源
    private static void BuildAsset(string path, string abName)
    {
        foreach (var file in Directory.GetFiles(path))
        {
            if (Path.GetExtension(file) == ".meta")
                continue;
            AssetImporter importer = AssetImporter.GetAtPath(file.Substring(file.IndexOf("Asset")));
            if (importer != null)
            {
                importer.assetBundleName = abName;
            }
        }

        foreach (var directory in Directory.GetDirectories(path))
        {
            if (Path.GetExtension(directory) == ".meta")
                continue;
            BuildAsset(directory, abName);
        }
    }

    #endregion
}
