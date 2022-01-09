using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ActionEditor : EditorWindow
{
    public ColliderInfos colliderInfo
    {
        set
        {
            if (m_colliderInfo != value)
            {
                m_colliderInfo = value;
                if (m_colliderInfo != null && m_colliderInfo.frameCollInfos.Count == 0 && m_curanimationData != null)
                {
                    m_colliderInfo.frameCount = m_curanimationData.frameList.Count;
                }
                colliderInfosEditor = Editor.CreateEditor(colliderInfo) as ColliderInfoEditor;
            }
        }

        get
        {
            return m_colliderInfo;
        }
    }

    public AnimationData animationData;

    private AnimationData m_curanimationData;

    private ColliderInfoEditor colliderInfosEditor;

    private ColliderInfos m_colliderInfo;



    [MenuItem("Tools/2DGame/ActionEditor")]
    static void Init()
    {

        ActionEditor actionEditor = (ActionEditor)EditorWindow.GetWindow(typeof(ActionEditor));
        actionEditor.titleContent = new GUIContent("ActionEditor");
        actionEditor.Show();

        
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 300, 800), string.Empty, EditorStyles.helpBox);

        GUI.Label(new Rect(30, 14, 80, 20), "动画数据:");
        animationData = (AnimationData)EditorGUI.ObjectField(new Rect(95, 15, 150, 20), animationData, typeof(AnimationData), true);

        GUI.Label(new Rect(30, 54, 80, 20), "碰撞信息:");
        colliderInfo = (ColliderInfos)EditorGUI.ObjectField(new Rect(95, 55, 150, 20), colliderInfo, typeof(ColliderInfos), true);


        if (animationData == null || colliderInfo == null)
        {
            GUIStyle errorStyle = new GUIStyle();
            errorStyle.normal.textColor = Color.red;
            errorStyle.fontSize = 20;
            GUI.Label(new Rect(40, 150, 200, 100), "请拖拽对应的数据赋值", errorStyle);
            return;
        }
        else
        {
            string aniName = animationData.name.Split('_')[0];
            string collName = colliderInfo.name.Split('_')[0];
            if (string.Compare(aniName, collName) == -1)
            {
                EditorGUI.HelpBox(new Rect(40, 150, 200, 100), "放入的数据名称前缀不相符，请检查是否同一组数据！", MessageType.Error);
                return;
            }
        }


        GUI.Label(new Rect(30, 100, 80, 20), "碰撞数量:");


        if (GUI.Button(new Rect(95, 100, 50, 22), "+"))
        {
            if (colliderInfosEditor == null)
                return;

            colliderInfosEditor.AddColliderInfo();
        }

        if (GUI.Button(new Rect(195, 100, 50, 22), "-"))
        {
            if (colliderInfosEditor == null)
                return;

            colliderInfosEditor.RemoveColliderInfo(colliderInfosEditor.curSelectCollBox);
        }

        if (GUI.Button(new Rect(30, 150, 215, 22), "清除所有碰撞"))
        {
            if (colliderInfosEditor == null)
                return;

            colliderInfosEditor.ClearColliederInfo();
        }

        if (GUI.Button(new Rect(30, 180, 100, 22), "Copy"))
        {
            if (colliderInfosEditor == null)
                return;

            colliderInfosEditor.CopyValue(colliderInfosEditor.curSelectCollBox);
        }

        if (GUI.Button(new Rect(145, 180, 100, 22), "Paste"))
        {
            if (colliderInfosEditor == null)
                return;

            colliderInfosEditor.PasteValue(colliderInfosEditor.curSelectCollBox);
        }

        GUI.Label(new Rect(30, 220, 120, 20), string.Format("动画总帧数：{0}", animationData.frameList.Count));



        GUILayout.BeginArea(new Rect(350, 20, 550, 2000));
        if (colliderInfosEditor != null)
        {
            colliderInfosEditor.animationData = animationData;
            colliderInfosEditor.OnInspectorGUI();
        }
        GUILayout.EndArea();


    }

}