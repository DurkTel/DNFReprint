using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class Entity : GMUpdateCollider.IColliderInfo
{
    /// <summary>
    /// XY轴碰撞盒父节点
    /// </summary>
    private Transform m_collidersXY_parent;
    /// <summary>
    /// Z轴碰撞盒父节点
    /// </summary>
    private Transform m_collidersZ_parent;
    /// <summary>
    /// 已生成的碰撞盒数量（单轴）
    /// </summary>
    private int m_maxCount;
    /// <summary>
    /// Z轴触发器列表
    /// </summary>
    protected List<ColliderTrigger> m_triggerZ = new List<ColliderTrigger>();
    /// <summary>
    /// XY轴触发器列表
    /// </summary>
    protected List<ColliderTrigger> m_triggerXY = new List<ColliderTrigger>();
    /// <summary>
    /// XY轴碰撞盒列表
    /// </summary>
    protected List<BoxCollider2D> m_collidersXY = new List<BoxCollider2D>();
    /// <summary>
    /// Z轴碰撞盒列表
    /// </summary>
    protected List<BoxCollider2D> m_collidersZ = new List<BoxCollider2D>();
    public bool updateColliderEnabled { get; set; }
    public FrameCollInfo frameCollInfo { get; private set; }
    public GMUpdateCollider colliderUpdate { get; set; }
    public ColliderInfos own_colliderInfo { get; set; }
    public List<ColliderTrigger> triggerZ { get => m_triggerZ; }
    public List<ColliderTrigger> triggerXY { get => m_triggerXY; }
    public List<BoxCollider2D> collidersZ { get => m_collidersZ; }
    public List<BoxCollider2D> collidersXY { get => m_collidersXY; }
    public Transform collidersXY_parent { get => m_collidersXY_parent; }
    public Transform collidersZ_parent { get => m_collidersZ_parent; }

    public static UnityAction<ColliderTrigger, ColliderTrigger, int> onContactHandlerEvent;

    public virtual void ContactHandle(GMUpdateCollider.ContactPair contact, ColliderInfos collInfo)
    {
        onContactHandlerEvent?.Invoke(contact.attacker, contact.victim, collInfo.skillCode);
    }

    private void ColliderInit()
    {
        updateColliderEnabled = true;
        GameObject colliders_XY = new GameObject("colliders_XY");
        GameObject colliders_Z = new GameObject("colliders_Z");
        colliders_XY.transform.SetParent(transform, false);
        colliders_Z.transform.SetParent(transform, false);
        m_collidersXY_parent = colliders_XY.transform;
        m_collidersZ_parent = colliders_Z.transform;
        m_collidersXY_parent.localScale = Vector3.one * 0.01f;
        m_collidersZ_parent.localScale = Vector3.one * 0.01f;

        updateAnimationEvent += InitCollider;

        updateSpriteEvent += RefreshCollider;
    }

    private void ReleaseCollider()
    {
        updateColliderEnabled = false;
        updateAnimationEvent -= InitCollider;
        updateSpriteEvent -= RefreshCollider;
        m_collidersZ.Clear();
        m_collidersXY.Clear();
        m_triggerXY.Clear();
        m_triggerZ.Clear();
        Object.Destroy(m_collidersXY_parent.gameObject);
        Object.Destroy(m_collidersZ_parent.gameObject);
    }


    private void InitCollider(AnimationData animationData)
    {
        current_animationData = animationData;
        if (current_animationData.colliderInfo == null || !updateColliderEnabled) return;
        own_colliderInfo = current_animationData.colliderInfo;

        m_maxCount = 0;
        if (own_colliderInfo != null)
        {
            foreach (var frame in own_colliderInfo.frameCollInfos)
            {
                if (m_maxCount < frame.single_colliderInfo.Count)
                    m_maxCount = frame.single_colliderInfo.Count;
            }
        }

        //根据该动画的最大碰撞盒数创建碰撞盒
        if (m_collidersXY.Count < m_maxCount)
        {
            int length = m_maxCount - m_collidersXY.Count;
            for (int i = 0; i < length; i++)
            {
                CreateCollider(m_collidersXY, m_triggerXY, GMUpdateCollider.Axial.AxialXY, own_colliderInfo);
                CreateCollider(m_collidersZ, m_triggerZ, GMUpdateCollider.Axial.AxialZ, own_colliderInfo);
            }
        }

        //更新信息
        for (int i = 0; i < m_triggerXY.Count; i++)
        {
            m_triggerXY[i].colliderInfos = own_colliderInfo;
            m_triggerZ[i].colliderInfos = own_colliderInfo;
        }
    }

    private void RefreshCollider(int currentFrame)
    {
        if (own_colliderInfo == null)
        {
            own_colliderInfo = current_animationData.colliderInfo;
        }

        if (own_colliderInfo == null || !updateColliderEnabled) return;
        int totalCollFrame = own_colliderInfo.frameCollInfos.Count;
        if (currentFrame >= totalCollFrame) return;
        colliderUpdate.ClearContactZ(entityId);

        frameCollInfo = own_colliderInfo.frameCollInfos[currentFrame];

        int isFilp = curFlip;
        Vector3 collScale = new Vector3(isFilp, 1, 1);

        int allCount = m_collidersXY.Count;
        int singleCount = frameCollInfo.single_colliderInfo.Count;

        for (int i = 0; i < allCount; i++)
        {
            bool isActive = i < singleCount;
            if (i < singleCount)
            {
                ColliderInfo info = frameCollInfo.single_colliderInfo[i];
                m_collidersXY[i].offset = info.offset;
                m_collidersXY[i].size = info.size;
                m_collidersXY[i].isTrigger = info.isTrigger;
                //m_collidersXY[i].gameObject.layer = (int)info.layer + 9;
                m_collidersXY[i].transform.localScale = collScale;
                m_triggerXY[i].hashCode = frameCollInfo.single_colliderInfo[i].GetHashCode();
                m_triggerXY[i].layer = info.layer;

                m_collidersZ[i].offset = new Vector2(info.offset.x, info.offset_Z);
                m_collidersZ[i].size = new Vector2(info.size.x, info.size_Z);
                m_collidersZ[i].isTrigger = info.isTrigger;
                //m_collidersZ[i].gameObject.layer = (int)info.layer + 9;
                m_collidersZ[i].transform.localScale = collScale;
                m_triggerZ[i].hashCode = frameCollInfo.single_colliderInfo[i].GetHashCode();
                m_triggerZ[i].layer = info.layer;

            }
            m_collidersXY[i].enabled = isActive;
            m_collidersZ[i].enabled = isActive;
            m_triggerXY[i].enabled = isActive;
            m_triggerZ[i].enabled = isActive;

        }
    }

    protected void CreateCollider(List<BoxCollider2D> boxList, List<ColliderTrigger> triggerList, GMUpdateCollider.Axial axial, ColliderInfos colliderInfos = null)
    {
        string name = axial == GMUpdateCollider.Axial.AxialXY ? "collider_XY" : "collider_Z";
        Transform parent = axial == GMUpdateCollider.Axial.AxialXY ? m_collidersXY_parent : m_collidersZ_parent;
        GameObject newCollider = new GameObject(name);
        BoxCollider2D newColl = newCollider.AddComponent<BoxCollider2D>();
        ColliderTrigger newCollTrigger = newCollider.AddComponent<ColliderTrigger>();
        newCollTrigger.entity = this;
        newCollTrigger.axial = axial;
        newCollTrigger.colliderInfos = colliderInfos;
        triggerList.Add(newCollTrigger);
        newCollider.transform.SetParent(parent, false);
        boxList.Add(newColl);
    }


    public void AddEntitySkill()
    { 
        
    }

    public struct ContentDamageHit
    {
        public int hitCount;

        public float lastHitTime;
    }
}
