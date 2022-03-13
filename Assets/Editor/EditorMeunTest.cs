using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class EditorMeunTest
{
    static string datapath = "Assets/Prefabs";

    [MenuItem("遍历所有预制体/Start")]
    private static void Test()
    {
        Find();
    }

    private static void Find()
    { 
        
        if(Directory.Exists(datapath))
        {
            var absolutePaths = Directory.GetFiles(datapath, "*.prefab", SearchOption.AllDirectories);
            for (int i = 0; i < absolutePaths.Length; i++)
            {
                string path = absolutePaths[i];
                path = path.Replace("\\", "/");
                GameObject prefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
                if (prefab != null)
                {
                    //SpriteAnimator spriteAnimator = prefab.GetComponentInChildren<SpriteAnimator>();
                    //if (spriteAnimator != null && spriteAnimator.m_stop)
                    //{
                    //    Debug.Log(prefab.name);
                    //}
                }
            }
        }
    }

}
