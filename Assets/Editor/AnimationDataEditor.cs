using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(AnimationData))]
public class AnimationDataEditor : Editor
{

    private Rect m_TotalSize;

    private SerializedProperty m_AnimationData;

    private SerializedProperty m_FrameList;

    private SerializedProperty m_SelectIndex;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //m_AnimationData = serializedObject.FindProperty("animationData");

        m_FrameList = serializedObject.FindProperty("frameList");

        m_SelectIndex = serializedObject.FindProperty("selectedIndex");

        Rect m_TotalSize = GUILayoutUtility.GetRect(1000, 3000);

        if (GUI.Button(new Rect(1.5f, 140, (m_TotalSize.width - 50) / 2.0f, 20), "+"))
        {
            if (m_FrameList.arraySize == 0)
            {
                m_SelectIndex.intValue = 0;
                m_FrameList.InsertArrayElementAtIndex(m_SelectIndex.intValue);

            }
            else
            {
                m_FrameList.InsertArrayElementAtIndex(m_SelectIndex.intValue);
                m_SelectIndex.intValue++;
            }
            Debug.Log(m_SelectIndex.intValue);
            SerializedProperty citem = m_FrameList.GetArrayElementAtIndex(m_SelectIndex.intValue);
            EditorGUIUtility.PingObject(citem.FindPropertyRelative("sprite").objectReferenceValue);
            citem.FindPropertyRelative("sprite").objectReferenceValue = null;
            //citem.FindPropertyRelative("EVENTs").arraySize = 0;
            //citem.FindPropertyRelative("eventEnabled").boolValue = false;
            //citem.FindPropertyRelative("eventName").stringValue = string.Empty;
            citem.FindPropertyRelative("interval").floatValue = 0.1f;
        }


        SerializedProperty current = m_FrameList.GetArrayElementAtIndex(0);


        //SerializedProperty mEventName = current.FindPropertyRelative("eventName");
        //mEventName.stringValue = GUI.TextField(new Rect(175, 170, 100, 20), mEventName.stringValue);

        //SerializedProperty mEventEnabled = current.FindPropertyRelative("eventEnabled");
        //mEventEnabled.boolValue = GUI.Toggle(new Rect(190, 200, 20, 20), mEventEnabled.boolValue,new GUIContent());

        SerializedProperty mSprite = current.FindPropertyRelative("sprite");
        EditorGUI.PropertyField(new Rect(145, 70, 100, 20), mSprite, new GUIContent(string.Empty));




        Sprite sp = mSprite.objectReferenceValue as Sprite;
        if (sp)
            GUI.DrawTextureWithTexCoords(new Rect(145, 260, sp.rect.width, sp.rect.height), sp.texture, new Rect(0, 0, 1, 1));



    }

}
