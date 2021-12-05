using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class SpriteAnimationEditorWindow : EditorWindow
{
    public static AnimationConfig animationConfig;
    public Texture texture;

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, maxSize.x, maxSize.y), texture);
        EditorGUILayout.BeginHorizontal();
        //DataEditorInspector();
        DrawEditorInspector();
        GUILayout.Label("Select SpriteAnimator", EditorStyles.boldLabel);

        DrawColliderInspector();
    }

    private void DrawEditorInspector()
    {
        EditorGUILayout.Space(15);

    }

    /// <summary>
    /// 碰撞盒面板
    /// </summary>
    public void DrawColliderInspector()
    {

        EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(330));
        EditorGUILayout.Space(18);
        //right = EditorGUILayout.BeginScrollView(right, GUILayout.Width(330));


        //if (currentSelectFrame != null)
        //{
        //    Editor.CreateEditor((Object)currentSelectFrame).OnInspectorGUI();
        //}
        DrawTime();
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    private void DrawTime()
    {
        Repaint();
        Rect rcTimeLine = new Rect(2, 2, position.width - 2 - 2 - 14, position.height - 2);

        //if (HitboxManager != null && HitboxManager.m_Animations.Length > 0)
            TimeScrubberWindow.TimeScrubber(rcTimeLine);

    }


    [MenuItem("Tools/SpriteAnimation")]
    public static void OpenWindow()
    {
        SpriteAnimationEditorWindow window = EditorWindow.GetWindow(typeof(SpriteAnimationEditorWindow)) as SpriteAnimationEditorWindow;
        window.titleContent = new GUIContent("SpriteAnimation");
        window.Show();

        //spriteAnimator = null;
    }
}
