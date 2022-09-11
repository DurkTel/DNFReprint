using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AnimationMap))]
public class AnimationMapEditior : Editor
{
    private AnimationMap animationMap;

    public void OnEnable()
    {
        animationMap = target as AnimationMap;
        if (animationMap.animations.Count <= 0)
        {
            string[] totalNames = Enum.GetNames(typeof(AnimationMap.AnimationEnum));
            animationMap.names = new List<string>();
            animationMap.animations = new List<AnimationData>();
            for (int i = 0; i < totalNames.Length; i++)
            {
                animationMap.AddAnimation(totalNames[i], null);
            }
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
            animationMap.aniType[i] = (AnimationMap.AniType)EditorGUILayout.EnumFlagsField(animationMap.aniType[i]);
            animationMap.animations[i] = (AnimationData)EditorGUILayout.ObjectField(animationMap.animations[i], typeof(AnimationData), true);
            EditorGUILayout.EndHorizontal();

        }
    }
}
