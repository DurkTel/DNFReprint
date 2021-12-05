using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Test : EditorWindow
{
    public GameObject target;

    private GameObject m_curtarget;

    public AnimationData animationData;

    private AnimationData m_curanimationData;

    public Texture texture;

    private RenenderSprite[] childRenders;

    private int m_showIndex;

    private int m_curIndex;

    private int m_beginIndex;


    [MenuItem("Tools/Test")]
    static void Init()
    {

        Test test = (Test)EditorWindow.GetWindow(typeof(Test));
        test.titleContent = new GUIContent("ActionEditor");
        test.Show();
    }
    
    private void OnGUI()
    {
        EditorGUILayout.BeginVertical("box");
        target = (GameObject)EditorGUILayout.ObjectField(target, typeof(GameObject), true);
        animationData = (AnimationData)EditorGUILayout.ObjectField(animationData, typeof(AnimationData), true);

        if (animationData == null || target == null)
            return;

        InitSprite();
        InitIndex();

        if (GUI.Button(new Rect(250, 50, 50, 22), "下一帧"))
        {
            if (animationData == null || target == null)
            {
                Debug.LogError("请先赋值");
                return;
            }

            if (m_curIndex == animationData.frameList.Count - 1)
                return;
            m_curIndex++;

            RefreshSprite(m_curIndex);
        }

        if (GUI.Button(new Rect(30, 50, 50, 22), "上一帧"))
        {
            if (animationData == null || target == null)
            {
                Debug.LogError("请先赋值");
                return;
            }

            if (m_curIndex == 0)
                return;
            m_curIndex--;
            
            RefreshSprite(m_curIndex);
        }

        GUI.Label(new Rect(90, 50, 200, 22), "当前帧数:"+ (m_curIndex + 1));

        //GUI.DrawTexture(new Rect(90, 90, texture.width, texture.height), texture);
        EditorGUILayout.EndVertical();
    }

    private void InitSprite()
    {
        if (target != null && target != m_curtarget)
        {
            childRenders = target.GetComponentsInChildren<RenenderSprite>();
            if (childRenders.Length <= 0)
            { 
                Debug.LogError("预制体里没有挂载任何SpriteRenderer脚本");
                return;
            }
            foreach (RenenderSprite render in childRenders)
            {
                //初始化
                render.InitSpriteForEditor();
            }
            m_curtarget = target;
        }
    }

    private void InitIndex()
    { 
        if (animationData != null && animationData != m_curanimationData)
        {
            m_beginIndex = int.Parse(animationData.frameList[0].sprite.name);
            m_curIndex = 0;
            m_curanimationData = animationData;
        }
    }

    private void RefreshSprite(int index)
    {
        if (m_showIndex == index) return;
        foreach (RenenderSprite render in childRenders)
        {
            render.SetSprite(m_beginIndex + index);
        }
        m_showIndex = index;
    }
}