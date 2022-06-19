using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System;

public class BuildVersionFiles
{

    [MenuItem("Assets/Build/Build VersionFiles")]
    private static void Build()
    {
        string path = Application.streamingAssetsPath;

        string filePath = path + "/file.txt";
        string versionPath = path + "/version.txt";

        if (File.Exists(filePath))
            File.Delete(filePath);

        if (File.Exists(versionPath))
            File.Delete(versionPath);

        StringBuilder file_str = new StringBuilder();
        string[] files = Directory.GetFiles(path);

        for (int i = 0; i < files.Length; i++)
        {
            if (Path.GetExtension(files[i]) != ".meta" && Path.GetExtension(files[i]) != ".txt")
            {
                string name = Path.GetFileName(files[i]);
                string md5 = GetMD5(files[i]);
                file_str.Append(string.Format("{0}|{1}\n", name, md5));

            }
        }
        File.WriteAllText(filePath, file_str.ToString());
        File.WriteAllText(versionPath, "version|1.00");

        AssetDatabase.Refresh();
    }


    private static string GetMD5(string file)
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
        catch (Exception ex)
        {
            throw new Exception("md5file() fail, error:" + ex.Message);
        }
    }
}
