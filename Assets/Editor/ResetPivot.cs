using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ResetPivot : EditorWindow
{
    public static string path = "Character/swordman-f/body";

    public static Vector2 m_anchorVector;

    public static Vector2 offset;

    private static string[] m_coordinate;


    [MenuItem("Tools/ResetPivot")]
    private static void OpenGui()
    {
        ResetPivot resetPivot = (ResetPivot)EditorWindow.GetWindow(typeof(ResetPivot));
        resetPivot.titleContent = new GUIContent("ResetPivotEditor");
        resetPivot.maxSize = new Vector2(350, 200);
        resetPivot.Show();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 20), "请输入资源文件夹路径：");

        path = GUI.TextField(new Rect(10, 50, 300, 20), path, 25);

        if (GUI.Button(new Rect(80, 100, 150, 35), "开始批量处理中心点偏移"))
        {
           
            InitSprite();
        }
    }

    private static void InitSprite()
    {
        if (path == "")
        {
            Debug.LogError("路径为空！");
            return;
        }

        TextAsset tempTA = Resources.Load<TextAsset>(path + "/pointOffsize");
        TextAsset offsetTA = Resources.Load<TextAsset>(path + "/offset");

        if (!tempTA || !offsetTA)
        {
            Debug.LogError("路径下没有资源！");
            return;
        }

        string str = tempTA.ToString();
        string strOffset = offsetTA.ToString();
        m_coordinate = str.Split(' ', '\n');
        string[] m_offset = strOffset.Split(' ');
        offset = new Vector2(int.Parse(m_offset[0]), int.Parse(m_offset[1]));

        int m_spriteCount = m_coordinate.Length / 2;

        Sprite m_singSprite;

        for (int i = 0; i < m_spriteCount; i++)
        {
            m_singSprite = Resources.Load<Sprite>(path + '/' + i);
            string textPath = "Assets/Resources/" + path + '/' + i + ".png";
            TextureImporter textureImporter = AssetImporter.GetAtPath(textPath) as TextureImporter;


            if (m_singSprite == null)
            {
                Debug.LogError("资源里图片和中心配置表数量不对，开始超出范围是" + i);
                return;
            }

            m_anchorVector.x = int.Parse(m_coordinate[i * 2]);
            m_anchorVector.y = int.Parse(m_coordinate[i * 2 + 1]);

            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.spritePivot = GetPiovt(m_singSprite);
            AssetDatabase.ImportAsset(textPath);
        }

    }


    private static Vector2 GetPiovt(Sprite singSprite)
    {
        Vector2 newPivot = Vector2.zero;

        newPivot = new Vector2(0.5f - ((m_anchorVector.x - offset.x + singSprite.rect.width / 2) / singSprite.rect.width),
                0.5f + ((m_anchorVector.y - offset.y + singSprite.rect.height / 2) / singSprite.rect.height));

        return newPivot;
    }
}
