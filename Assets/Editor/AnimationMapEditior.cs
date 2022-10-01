using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AnimationMap))]
public class AnimationMapEditior : Editor
{
    private AnimationMap animationMap;

    private List<bool> toggle = new List<bool>();

    public void OnEnable()
    {
        animationMap = (AnimationMap)target;
        toggle.Clear();
        for (int i = 0; i < animationMap.animations.Count; i++)
        {
            toggle.Add(false);
        }
        
        //if (animationMap.names.Count <= 0)
        //{
        //    string[] totalNames = Enum.GetNames(typeof(AnimationMap.AnimationEnum));
        //    for (int i = 0; i < totalNames.Length; i++)
        //    {
        //        animationMap.AddAnimation(totalNames[i], null);
        //    }
        ////}
        //for (int i = 0; i < animationMap.names.Count; i++)
        //{
        //    if (!totalNames.Contains(animationMap.names[i]))
        //        animationMap.RemoveAnimation(animationMap.names[i]);
        //}
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("动画名称");
        EditorGUILayout.LabelField("动画类型");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(5);
        for (int i = 0; i < animationMap.names.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();
            toggle[i] = EditorGUILayout.Toggle(toggle[i], GUILayout.Width(20));
            animationMap.names[i] = EditorGUILayout.TextField(animationMap.names[i]);
            animationMap.animationFlags[i] = (AnimationMap.AniType)EditorGUILayout.EnumFlagsField(animationMap.animationFlags[i]);
            animationMap.animations[i] = (AnimationData)EditorGUILayout.ObjectField(animationMap.animations[i], typeof(AnimationData), false);
            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(animationMap);

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space(30);
        EditorGUILayout.BeginHorizontal();

        EditorGUI.BeginChangeCheck();
        if (GUILayout.Button("+"))
        {
            toggle.Add(false);
            animationMap.AddAnimation("", null);
        }
        if (GUILayout.Button("-"))
        {
            for (int i = toggle.Count - 1; i >= 0; i--)
            {
                if (toggle[i] == true)
                {
                    toggle.RemoveAt(i);
                    animationMap.RemoveAnimation(i);
                }
            }
        }
        if (EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(animationMap);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();

    }
}
