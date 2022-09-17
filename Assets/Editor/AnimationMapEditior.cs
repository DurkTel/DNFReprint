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

    public void OnEnable()
    {
        animationMap = (AnimationMap)target;
        //if (animationMap.names.Count <= 0)
        //{
            string[] totalNames = Enum.GetNames(typeof(AnimationMap.AnimationEnum));
            for (int i = 0; i < totalNames.Length; i++)
            {
                animationMap.AddAnimation(totalNames[i], null);
            }
        //}
        for (int i = 0; i < animationMap.names.Count; i++)
        {
            if (!totalNames.Contains(animationMap.names[i]))
                animationMap.RemoveAnimation(animationMap.names[i]);
        }
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("动画名称");
        EditorGUILayout.LabelField("动画类型");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(5);
        for (int i = 0; i < animationMap.names.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(animationMap.names[i]);
            animationMap.animationFlags[i] = (AnimationMap.AniType)EditorGUILayout.EnumFlagsField(animationMap.animationFlags[i]);
            animationMap.animations[i] = (AnimationData)EditorGUILayout.ObjectField(animationMap.animations[i], typeof(AnimationData), false);
            EditorUtility.SetDirty(animationMap);
            EditorGUILayout.EndHorizontal();
        }
    }
}
