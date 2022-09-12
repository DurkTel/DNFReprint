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
        if (animationMap.animationPiars == null || animationMap.animationPiars.Count <= 0)
        {
            string[] totalNames = Enum.GetNames(typeof(AnimationMap.AnimationEnum));
            animationMap.names = new List<string>();
            animationMap.animationPiars = new List<AnimationMap.AnimationPiars>();
            for (int i = 0; i < totalNames.Length; i++)
            {
                animationMap.AddAnimation(totalNames[i], new AnimationMap.AnimationPiars());
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
            animationMap.animationPiars[i].aniType = (AnimationMap.AniType)EditorGUILayout.EnumFlagsField(animationMap.animationPiars[i].aniType);
            animationMap.animationPiars[i].animations = (AnimationData)EditorGUILayout.ObjectField(animationMap.animationPiars[i].animations, typeof(AnimationData), true);
            EditorGUILayout.EndHorizontal();
        }
    }
}
