using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCollider : MonoBehaviour
{
    private SpriteAnimator m_spriteAnimator;

    private AnimationData m_animationData;

    private ColliderInfo m_colliderInfo;

    private FrameCollInfo m_frameCollInfo;

    /// <summary>
    /// XY轴
    /// </summary>
    private List<BoxCollider2D> m_collidersXY = new List<BoxCollider2D>();

    /// <summary>
    /// 虚拟Z轴
    /// </summary>
    private List<BoxCollider2D> m_collidersZ = new List<BoxCollider2D>();

    private Transform m_collidersXY_parent;

    private Transform m_collidersZ_parent;

    void Start()
    {
        m_spriteAnimator = GetComponentInChildren<SpriteAnimator>();
        AddUpdateEvent();
        m_collidersXY_parent = transform.Find("Colliders").transform.Find("colliders_XY");
        m_collidersZ_parent = transform.Find("Colliders").transform.Find("colliders_Z");
    }

    void Update()
    {
        
    }

    private void AddUpdateEvent()
    {
        m_spriteAnimator.UpdateAnimationEvent += InitCollider;

        m_spriteAnimator.UpdateSpriteEvent += RefreshCollider;
    }

    private void InitCollider(AnimationData animationData)
    {
        m_animationData = animationData;
        //if (m_animationData.colliderInfo == null) return;
        m_colliderInfo = m_animationData.colliderInfo;

        int maxCout = 0;
        if (m_colliderInfo != null)
        {
            foreach (FrameCollInfo frame in m_colliderInfo.frameCollInfos)
            {
                if (maxCout < frame.collValueConfigs.Count)
                    maxCout = frame.collValueConfigs.Count;
            }
        }

        //根据该动画的最大碰撞盒数创建碰撞盒
        if (m_collidersXY.Count < maxCout)
        {
            int length = maxCout - m_collidersXY.Count;
            for (int i = 0; i < length; i++)
            {
                GameObject newColliderXY = new GameObject("collider_XY" + i);
                BoxCollider2D newCollXY = newColliderXY.AddComponent<BoxCollider2D>();
                newColliderXY.transform.SetParent(m_collidersXY_parent, false);
                m_collidersXY.Add(newCollXY);

                GameObject newColliderZ = new GameObject("collider_Z" + i);
                BoxCollider2D newCollZ = newColliderZ.AddComponent<BoxCollider2D>();
                newColliderZ.transform.SetParent(m_collidersZ_parent, false);
                m_collidersZ.Add(newCollZ);
            }

            for (int i = 0; i < m_collidersXY.Count; i++)
            {
                m_collidersXY[i].gameObject.SetActive(true);
                m_collidersZ[i].gameObject.SetActive(true);
            }
        }
        else if (m_collidersXY.Count == maxCout)
        {
            for (int i = 0; i < m_collidersXY.Count; i++)
            {
                m_collidersXY[i].gameObject.SetActive(true);
                m_collidersZ[i].gameObject.SetActive(true);
            }
        }
        else
        {
            int more = m_collidersXY.Count - maxCout;
            for (int i = more - 1; i >= 0; i--)
            {
                m_collidersXY[i].gameObject.SetActive(false);
                m_collidersZ[i].gameObject.SetActive(false);
            }
        }
    }

    private void RefreshCollider(int currentFrame)
    {
        if (m_colliderInfo == null)
        {
            m_colliderInfo = m_spriteAnimator.current_animationData.colliderInfo;
        }

        if (m_colliderInfo == null) return;
        int totalCollFrame = m_colliderInfo.frameCollInfos.Count;
        if (currentFrame > totalCollFrame) return;

        m_frameCollInfo = m_colliderInfo.frameCollInfos[currentFrame];

        for (int i = 0; i < m_collidersXY.Count; i++)
        {
            m_collidersXY[i].offset = m_frameCollInfo.collValueConfigs[i].offset;
            m_collidersXY[i].size = m_frameCollInfo.collValueConfigs[i].size;

            m_collidersZ[i].offset = new Vector2(m_frameCollInfo.collValueConfigs[i].offset.x, m_frameCollInfo.collValueConfigs[i].offset_Z);
            m_collidersZ[i].size = new Vector2(m_frameCollInfo.collValueConfigs[i].size.x, m_frameCollInfo.collValueConfigs[i].size_Z);
        }

    }


}
