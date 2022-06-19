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

        string newPath = Application.dataPath + "/LuaTemp/";

        List<string> pathList = new List<string>();
        CopyAllLuaFile(AssetDefine.luaPath, newPath, ref pathList);

        AssetDatabase.Refresh();

        foreach (var path in pathList)
        {
            AssetImporter importer = AssetImporter.GetAtPath(path.Substring(path.IndexOf("Asset")));
            if (importer != null)
            {
                importer.assetBundleName = "lua";
            }

        }

        BuildPipeline.BuildAssetBundles(abPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows);
        Directory.Delete(newPath, true);
        AssetDatabase.Refresh();

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
}
