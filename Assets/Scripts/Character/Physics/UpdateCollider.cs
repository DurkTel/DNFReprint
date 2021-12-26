using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCollider : MonoBehaviour
{
    private SpriteAnimator m_spriteAnimator;

    private RenenderSprite m_renenderSprite;

    private AnimationData m_animationData;

    private ColliderInfos m_colliderInfo;

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

    private void Awake()
    {
        m_spriteAnimator = GetComponentInChildren<SpriteAnimator>();
        m_renenderSprite = GetComponentInChildren<RenenderSprite>();
        AddUpdateEvent();
        m_collidersXY_parent = transform.Find("Colliders").transform.Find("colliders_XY");
        m_collidersZ_parent = transform.Find("Colliders").transform.Find("colliders_Z");
    }

    void Start()
    {
        
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
        //m_animationData = animationData;
        ////if (m_animationData.colliderInfo == null) return;
        //m_colliderInfo = m_animationData.colliderInfo;

        //int maxCout = 0;
        //if (m_colliderInfo != null)
        //{
        //    foreach (var frame in m_colliderInfo.frameCollInfos)
        //    {
        //        if (maxCout < frame.collValueConfigs.Count)
        //            maxCout = frame.collValueConfigs.Count;
        //    }
        //}

        ////根据该动画的最大碰撞盒数创建碰撞盒
        //if (m_collidersXY.Count < maxCout)
        //{
        //    int length = maxCout - m_collidersXY.Count;
        //    for (int i = 0; i < length; i++)
        //    {
        //        GameObject newColliderXY = new GameObject("collider_XY");
        //        BoxCollider2D newCollXY = newColliderXY.AddComponent<BoxCollider2D>();
        //        newColliderXY.transform.SetParent(m_collidersXY_parent, false);
        //        m_collidersXY.Add(newCollXY);

        //        GameObject newColliderZ = new GameObject("collider_Z");
        //        BoxCollider2D newCollZ = newColliderZ.AddComponent<BoxCollider2D>();
        //        newColliderZ.transform.SetParent(m_collidersZ_parent, false);
        //        m_collidersZ.Add(newCollZ);
        //    }

        //    for (int i = 0; i < m_collidersXY.Count; i++)
        //    {
        //        m_collidersXY[i].gameObject.SetActive(true);
        //        m_collidersZ[i].gameObject.SetActive(true);
        //    }
        //}
        //else if (m_collidersXY.Count == maxCout)
        //{
        //    for (int i = 0; i < m_collidersXY.Count; i++)
        //    {
        //        m_collidersXY[i].gameObject.SetActive(true);
        //        m_collidersZ[i].gameObject.SetActive(true);
        //    }
        //}
        //else
        //{
        //    //int more = m_collidersXY.Count - maxCout;
        //    //for (int i = m_collidersXY.Count - 1; i > maxCout - 1; i--)
        //    //{
        //    //    m_collidersXY[i].gameObject.SetActive(false);
        //    //    m_collidersZ[i].gameObject.SetActive(false);
        //    //}

        //    for (int i = 0; i < m_collidersXY.Count; i++)
        //    {
        //        bool isActive = maxCout - 1 >= i;
        //        m_collidersXY[i].gameObject.SetActive(isActive);
        //        m_collidersZ[i].gameObject.SetActive(isActive);
        //    }
        //}
    }

    private void RefreshCollider(int currentFrame)
    {
        //if (m_colliderInfo == null)
        //{
        //    m_colliderInfo = m_spriteAnimator.current_animationData.colliderInfo;
        //}

        //if (m_colliderInfo == null) return;
        //int totalCollFrame = m_colliderInfo.frameCollInfos.Count;
        //if (currentFrame > totalCollFrame) return;

        //m_frameCollInfo = m_colliderInfo.frameCollInfos[currentFrame];

        //int isFilp = m_renenderSprite.GetCurFlip();

        //for (int i = 0; i < m_frameCollInfo.collValueConfigs.Count; i++)
        //{
        //    m_collidersXY[i].offset = m_frameCollInfo.collValueConfigs[i].offset;
        //    m_collidersXY[i].size = m_frameCollInfo.collValueConfigs[i].size;
        //    m_collidersXY[i].isTrigger = m_frameCollInfo.collValueConfigs[i].isTrigger;
        //    m_collidersXY[i].gameObject.layer = (int)m_frameCollInfo.collValueConfigs[i].layer;
        //    m_collidersXY[i].transform.localScale = new Vector3(isFilp, 1, 1);

        //    m_collidersZ[i].offset = new Vector2(m_frameCollInfo.collValueConfigs[i].offset.x, m_frameCollInfo.collValueConfigs[i].offset_Z);
        //    m_collidersZ[i].size = new Vector2(m_frameCollInfo.collValueConfigs[i].size.x, m_frameCollInfo.collValueConfigs[i].size_Z);
        //    m_collidersZ[i].isTrigger = m_frameCollInfo.collValueConfigs[i].isTrigger;
        //    m_collidersZ[i].gameObject.layer = (int)m_frameCollInfo.collValueConfigs[i].layer;
        //    m_collidersZ[i].transform.localScale = new Vector3(isFilp, 1, 1);

        //}

    }


}
