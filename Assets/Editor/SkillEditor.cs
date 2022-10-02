using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SkillEditor : EditorWindow
{
    private AnimationData animationData;


    [MenuItem("Tools/2DGame/SkillEditor")]
    static void Init()
    {

        SkillEditor skillEditor = (SkillEditor)EditorWindow.GetWindow(typeof(SkillEditor));
        skillEditor.titleContent = new GUIContent("SkillEditor");
        skillEditor.Show();
    }

    private void OnEnable()
    {
        Selection.selectionChanged = () =>
        {
            animationData = Selection.activeObject as AnimationData;
            
        };

    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();


        EditorGUILayout.EndHorizontal();
    }
}
