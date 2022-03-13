using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

[CustomEditor(typeof(ColliderInfos))]
public class ColliderInfoEditor : Editor
{
    private SerializedProperty m_frameCollInfos;

    public SerializedProperty m_Colliders;

    public AnimationData animationData;

    private SerializedProperty m_frameCount;

    private SerializedProperty m_skillCode;

    private GUIStyle errorStyle;

    private int m_curSelectFrame = 1;

    public int curSelectCollBox = 0;

    private Vector2 m_scrollPos;

    public void OnEnable()
    {
        m_frameCollInfos = serializedObject.FindProperty("frameCollInfos");
        m_frameCount = serializedObject.FindProperty("frameCount");
        m_skillCode = serializedObject.FindProperty("skillCode");

        errorStyle = new GUIStyle();
        errorStyle.normal.textColor = Color.red;
        errorStyle.fontSize = 30;

    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        serializedObject.Update();
        m_scrollPos = EditorGUILayout.BeginScrollView(m_scrollPos, GUILayout.Width(550), GUILayout.Height(600));
        m_skillCode.intValue = EditorGUILayout.IntField("技能code：", m_skillCode.intValue, GUILayout.Width(500));
        m_frameCount.intValue = EditorGUILayout.IntField("总帧数：", m_frameCount.intValue, GUILayout.Width(500));



        if (animationData != null && m_frameCount.intValue > animationData.frameList.Count)
            m_frameCount.intValue = animationData.frameList.Count;
        else if(animationData != null && m_frameCount.intValue < 0)
            m_frameCount.intValue = 0;


        RefreshCount();

        if (m_frameCollInfos.arraySize > 0 && m_curSelectFrame <= m_frameCollInfos.arraySize)
        {
            m_curSelectFrame = EditorGUILayout.IntSlider("当前帧", m_curSelectFrame, 1, m_frameCollInfos.arraySize, GUILayout.Width(500));

            m_Colliders = m_frameCollInfos.GetArrayElementAtIndex(m_curSelectFrame - 1).FindPropertyRelative("single_colliderInfo");

            float rectHeight = m_Colliders.arraySize > 2 ? m_Colliders.arraySize  * 235: 250;
            GUILayoutUtility.GetRect(300, rectHeight);

            if (m_Colliders.arraySize > 0)
            {
                for (int i = 0; i < m_Colliders.arraySize; i++)
                {
                    SerializedProperty collider = m_Colliders.GetArrayElementAtIndex(i);
                    DrawBox(i, collider);
                }
            }
            else
            {
                GUI.Label(new Rect(18, 60, 200, 20), "当前帧没有碰撞参数", errorStyle);
            }

        }
        else
        {
            GUILayoutUtility.GetRect(300, 1000);
            GUI.Label(new Rect(18, 60, 200, 20), "碰撞尚未配置\n请先通过ActionEditor进行碰撞配置", errorStyle);
        }

        EditorGUILayout.EndScrollView();
        serializedObject.ApplyModifiedProperties();

    }

    #region 绘制界面
    private void RefreshCount()
    {
        if (m_frameCollInfos.arraySize > m_frameCount.intValue)
        {
            m_frameCollInfos.DeleteArrayElementAtIndex(m_frameCollInfos.arraySize - 1);
        }
        else if (m_frameCollInfos.arraySize < m_frameCount.intValue)
        {
            for (int i = 0; i < m_frameCount.intValue; i++)
            {
                if (m_frameCollInfos.arraySize <= 0)
                    m_frameCollInfos.InsertArrayElementAtIndex(0);
                else
                {
                    m_frameCollInfos.InsertArrayElementAtIndex(m_frameCollInfos.arraySize - 1);
                }
            }
        }
    }

    private void DrawBox(int index, SerializedProperty colliderInfo)
    {
        float posX = 300;
        float posY = 70 + index * 220;

        Rect bigBoxRect = new Rect(0, posY-5, 535, 210);

        Rect boxRect = new Rect(posX, posY, 230, 200);
        Color normalColor = GUI.color;

        GUI.color = curSelectCollBox == index ? Color.black : normalColor;
        GUI.Box(bigBoxRect, string.Empty, EditorStyles.helpBox);
        //选中
        if (bigBoxRect.Contains(Event.current.mousePosition))
        {
            if (Event.current.type == EventType.MouseDown)
            {
                curSelectCollBox = index;

            }
        }
        GUI.color = normalColor;


        GUI.Box(boxRect, string.Empty, EditorStyles.helpBox);
        GUILayout.BeginArea(boxRect);
        //偏移
        GUI.Label(new Rect(15, 14, 50, 20), "偏移值");
        SerializedProperty offset = colliderInfo.FindPropertyRelative("offset");
        offset.vector2Value = EditorGUI.Vector2Field(new Rect(60, 15, 150, 30),"", offset.vector2Value);

        //大小
        GUI.Label(new Rect(15, 44, 50, 20), "大小");
        SerializedProperty size = colliderInfo.FindPropertyRelative("size");
        size.vector2Value = EditorGUI.Vector2Field(new Rect(60, 45, 150, 30), "", size.vector2Value);

        //Z轴偏移
        GUI.Label(new Rect(15, 74, 50, 20), "Z轴偏移");
        SerializedProperty offset_Z = colliderInfo.FindPropertyRelative("offset_Z");
        offset_Z.floatValue = EditorGUI.FloatField(new Rect(72, 75, 138, 20), "", offset_Z.floatValue);

        //Z轴大小
        GUI.Label(new Rect(15, 104, 50, 20), "Z轴大小");
        SerializedProperty size_Z = colliderInfo.FindPropertyRelative("size_Z");
        size_Z.floatValue = EditorGUI.FloatField(new Rect(72, 105, 138, 20), "", size_Z.floatValue);

        //碰撞层级
        GUI.Label(new Rect(15, 134, 60, 20), "碰撞层级");
        SerializedProperty layer = colliderInfo.FindPropertyRelative("layer");
        layer.enumValueIndex = EditorGUI.Popup(new Rect(72, 135, 138, 20), layer.enumValueIndex, Enum.GetNames(typeof(ColliderLayer)));

        //应用Trigger
        GUI.Label(new Rect(15, 164, 70, 20), "应用Trigger");
        SerializedProperty isTrigger = colliderInfo.FindPropertyRelative("isTrigger");
        isTrigger.boolValue = EditorGUI.Toggle(new Rect(100, 166, 20, 20), isTrigger.boolValue);

        GUILayout.EndArea();

        DrawTextrue(offset.vector2Value, size.vector2Value, offset_Z.floatValue, size_Z.floatValue, index);

    }

    public void DrawTextrue(Vector2 offset, Vector2 size, float offsetZ, float sizeZ, int index)
    {
        float posX = 5;
        float posY = 70 + index * 220;

        if (animationData == null || animationData.frameList[m_curSelectFrame - 1] == null)
        {
            GUI.Label(new Rect(18, posY, 200, 100), "没有动画信息\n暂时无法展示效果图\n请通过ActionEditor进行配置");
            return;
        }

        Rect boxRect = new Rect(posX, posY, 230, 200);
        GUI.Box(boxRect, string.Empty, EditorStyles.helpBox);

        Texture texture = animationData.frameList[m_curSelectFrame - 1].sprite.texture;
        
        //计算中心点偏移
        float pivotX = animationData.frameList[m_curSelectFrame - 1].sprite.pivot.x;
        float pivotY = animationData.frameList[m_curSelectFrame - 1].sprite.pivot.y;
        float texturePosY = 200 - (texture.height - pivotY) + index * 220;

        GUI.DrawTexture(new Rect(120 - pivotX, texturePosY, texture.width, texture.height), texture);
        //有偏移 Y轴相反
        Vector2 newOffset = new Vector2(offset.x, -offset.y);
        //newOffset += new Vector2(130.1f, 206.9f + index * 220);
        newOffset += new Vector2(120.35f, 200.85f + index * 220);

        offsetZ = 200.85f + index * 220 - offsetZ;
        Handles.color = Color.green;
        Handles.DrawWireCube(newOffset, size);

        Handles.color = Color.red;
        Handles.DrawWireCube(new Vector2(newOffset.x,offsetZ), new Vector2(size.x, sizeZ));

    }
    #endregion

    #region 提供给外部编辑器的操作
    public void AddColliderInfo()
    {
        m_Colliders.InsertArrayElementAtIndex(0);
        serializedObject.ApplyModifiedProperties();
    }

    public void RemoveColliderInfo(int index)
    {
        if (index + 1 > m_Colliders.arraySize)
        {
            Debug.Log("请选中要移除的碰撞");
            return;
        }
        m_Colliders.DeleteArrayElementAtIndex(index);
        serializedObject.ApplyModifiedProperties();
    }

    public void ClearColliederInfo()
    {
        m_Colliders.arraySize = 0;
        serializedObject.ApplyModifiedProperties();
    }

    private Vector2 copyOffset;

    private Vector2 copySize;

    private float copyOffset_Z;

    private float copySize_Z;

    private int copyLayer;

    private bool copyIsTrigger;
    public void CopyValue(int index)
    {
        if (index + 1 > m_Colliders.arraySize)
        {
            Debug.Log("请选中要复制的碰撞");
            return;
        }
        SerializedProperty collider = m_Colliders.GetArrayElementAtIndex(index);

        SerializedProperty offset = collider.FindPropertyRelative("offset");
        SerializedProperty size = collider.FindPropertyRelative("size");
        SerializedProperty offset_Z = collider.FindPropertyRelative("offset_Z");
        SerializedProperty size_Z = collider.FindPropertyRelative("size_Z");
        SerializedProperty layer = collider.FindPropertyRelative("layer");
        SerializedProperty isTrigger = collider.FindPropertyRelative("isTrigger");


        copyOffset = offset.vector2Value;
        copySize = size.vector2Value;
        copyOffset_Z = offset_Z.floatValue;
        copySize_Z = size_Z.floatValue;
        copyLayer = layer.intValue;
        copyIsTrigger = isTrigger.boolValue;
    }

    public void PasteValue(int index)
    {
        if (index + 1 > m_Colliders.arraySize)
        {
            Debug.Log("请选中要赋值的碰撞");
            return;
        }
        SerializedProperty collider = m_Colliders.GetArrayElementAtIndex(index);

        SerializedProperty offset = collider.FindPropertyRelative("offset");
        SerializedProperty size = collider.FindPropertyRelative("size");
        SerializedProperty offset_Z = collider.FindPropertyRelative("offset_Z");
        SerializedProperty size_Z = collider.FindPropertyRelative("size_Z");
        SerializedProperty layer = collider.FindPropertyRelative("layer");
        SerializedProperty isTrigger = collider.FindPropertyRelative("isTrigger");

        offset.vector2Value = copyOffset;
        size.vector2Value = copySize;
        offset_Z.floatValue = copyOffset_Z;
        size_Z.floatValue = copySize_Z;
        layer.intValue = copyLayer;
        isTrigger.boolValue = copyIsTrigger;
        serializedObject.ApplyModifiedProperties();

    }
    #endregion
}

