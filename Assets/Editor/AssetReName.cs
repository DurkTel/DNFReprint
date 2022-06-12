using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class AssetReName : EditorWindow
{
    public static string name = "";

    [MenuItem("Tools/ReName")]
    private static void OpenGui()
    {
        AssetReName resetPivot = (AssetReName)EditorWindow.GetWindow(typeof(AssetReName));
        resetPivot.titleContent = new GUIContent("AssetReNameEditor");
        resetPivot.maxSize = new Vector2(350, 200);
        resetPivot.Show();
    }

    public static void ReName()
    {
        Object[] allFiles = Selection.GetFiltered<Object>(SelectionMode.DeepAssets);
        List<Object> list = new List<Object>();

        foreach (var item in allFiles)
        {
            string path = AssetDatabase.GetAssetPath(item);
            if (Path.GetExtension(path) == ".png")
            {
                list.Add(item);
                string name = Path.GetFileName(path);
            }
        }

        list.Sort((a, b) =>
        {
            string pathA = Path.GetFileName(AssetDatabase.GetAssetPath(a));
            string pathB = Path.GetFileName(AssetDatabase.GetAssetPath(b));
            int sortA = int.Parse(pathA.Substring(0, pathA.LastIndexOf('.')));
            int sortB = int.Parse(pathB.Substring(0, pathB.LastIndexOf('.')));

            if (sortA == sortB)
                return 0;

            int re = sortA > sortB ? 1 : -1;
            return re;
        });

        foreach (var item in list)
        {
            string path = AssetDatabase.GetAssetPath(item);
            string newName = name + "_" + item.name;
            AssetDatabase.RenameAsset(path, newName);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 20), "请输入重命名：");

        name = GUI.TextField(new Rect(10, 50, 300, 20), name, 80);

        if (GUI.Button(new Rect(80, 100, 150, 35), "开始执行"))
        {

            ReName();
        }
    }
}
