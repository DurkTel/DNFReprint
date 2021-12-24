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

    public ColliderInfo colliderInfo
    {
        set
        {
            m_colliderInfo = value;
        }

        get
        {
            if (m_colliderInfo != null && m_colliderInfo.frameCollInfos.Count == 0)
            {
                for (int i = 0; i < m_curanimationData.frameList.Count; i++)
                {
                    m_colliderInfo.frameCollInfos.Add(new FrameCollInfo());
                }
            }
            return m_colliderInfo;
        }
    }

    private ColliderInfo m_colliderInfo;

    public Texture texture;

    private RenenderSprite[] childRenders;

    private int m_showIndex;

    private int m_curIndex;

    private int m_beginIndex;

    private static float m_offsetX;

    private static float m_offsetY;

    private static float m_sizeX;

    private static float m_sizeY;

    private CollValueConfig m_collValue;




    [DrawGizmo(GizmoType.Active | GizmoType.InSelectionHierarchy)]
    private static void DrawCollBox(RenenderSprite target,GizmoType gizmoType)
    {
        var color = Gizmos.color;
        Gizmos.color = Color.green;

        Vector3 pos = target.gameObject.transform.position + new Vector3(m_offsetX, m_offsetY,0);

        Gizmos.DrawWireCube(pos, new Vector3(m_sizeX, m_sizeY, 0));
        Gizmos.color = color;
    }


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
        colliderInfo = (ColliderInfo)EditorGUILayout.ObjectField(colliderInfo, typeof(ColliderInfo), true);

        //colliderInfo.frameCollInfos.Add(new FrameCollInfo());

        if (animationData == null || target == null)
            return;

        InitSprite();
        InitIndex();

        if (GUI.Button(new Rect(250, 200, 50, 22), "下一帧"))
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

        if (GUI.Button(new Rect(30, 200, 50, 22), "上一帧"))
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

        if (GUI.Button(new Rect(150, 250, 70, 22), "添加碰撞"))
        {
            FrameCollInfo frameCollInfo = colliderInfo.frameCollInfos[m_curIndex];
            if (frameCollInfo.collValueConfigs == null)
                frameCollInfo.collValueConfigs = new List<CollValueConfig>();

            frameCollInfo.collValueConfigs.Add(new CollValueConfig());
        }

        if (GUI.Button(new Rect(30, 250, 70, 22), "减少碰撞"))
        {
            //FrameCollInfo frameCollInfo = colliderInfo.frameCollInfos[m_curIndex];
            //frameCollInfo.collValueConfigs.re
        }

        GUI.Label(new Rect(90, 200, 200, 22), "当前帧数:"+ (m_curIndex + 1));

        DrawCollValue();


        //GUI.DrawTexture(new Rect(90, 90, texture.width, texture.height), texture);
        EditorGUILayout.EndVertical();

    }

    private void DrawCollValue()
    {
        if (colliderInfo.frameCollInfos[m_curIndex].collValueConfigs == null) return;
            m_offsetX = EditorGUILayout.FloatField("X偏移量：", m_offsetX);
        m_offsetY = EditorGUILayout.FloatField("Y偏移量：", m_offsetY);
        m_sizeX = EditorGUILayout.FloatField("宽度：", m_sizeX);
        m_sizeY = EditorGUILayout.FloatField("长度：", m_sizeY);
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