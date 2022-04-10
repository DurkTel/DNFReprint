using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Entity : GMUpdateCollider.IColliderInfo
{
    private Transform m_collidersXY_parent;

    private Transform m_collidersZ_parent;

    private int m_maxCount;

    private List<ColliderTrigger> m_triggerZ = new List<ColliderTrigger>();

    private List<ColliderTrigger> m_triggerXY = new List<ColliderTrigger>();

    private List<BoxCollider2D> m_collidersXY = new List<BoxCollider2D>();

    private List<BoxCollider2D> m_collidersZ = new List<BoxCollider2D>();

    private List<ColliderInfos> m_contact_colliderInfos = new List<ColliderInfos>();
    public bool updateColliderEnabled { get; set; }
    public FrameCollInfo frameCollInfo { get; private set; }
    public GMUpdateCollider colliderUpdate { get; set; }
    public ColliderInfos own_colliderInfo { get; set; }
    public List<ColliderTrigger> triggerZ { get => m_triggerZ; }
    public List<ColliderTrigger> triggerXY { get => m_triggerXY; }
    public List<ColliderInfos> contact_colliderInfos { get => m_contact_colliderInfos; }
    public List<BoxCollider2D> collidersZ { get => m_collidersZ; }
    public List<BoxCollider2D> collidersXY { get => m_collidersXY; }
    public Transform collidersXY_parent { get => m_collidersXY_parent; }
    public Transform collidersZ_parent { get => m_collidersZ_parent; }

    public void OnGMUpdateColliderStayOut(bool inOut, Entity entity)
    {
        if (inOut == true)
        {
            Debug.LogFormat("进行碰撞！！！碰撞实体是：{0}", entity.gameObject.name);
        }
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

        int instanceID = own_colliderInfo.GetInstanceID();

        //根据该动画的最大碰撞盒数创建碰撞盒
        if (m_collidersXY.Count < m_maxCount)
        {
            int length = m_maxCount - m_collidersXY.Count;
            for (int i = 0; i < length; i++)
            {
                CreateCollider(m_collidersXY, m_triggerXY, GMUpdateCollider.Axial.AxialXY, instanceID);
                CreateCollider(m_collidersZ, m_triggerZ, GMUpdateCollider.Axial.AxialZ, instanceID);
            }

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
                m_collidersXY[i].gameObject.layer = (int)info.layer + 9;
                m_collidersXY[i].transform.localScale = collScale;

                m_collidersZ[i].offset = new Vector2(info.offset.x, info.offset_Z);
                m_collidersZ[i].size = new Vector2(info.size.x, info.size_Z);
                m_collidersZ[i].isTrigger = info.isTrigger;
                m_collidersZ[i].gameObject.layer = (int)info.layer + 9;
                m_collidersZ[i].transform.localScale = collScale;
            }
            m_collidersXY[i].enabled = isActive;
            m_collidersZ[i].enabled = isActive;
        }

    }

    private void CreateCollider(List<BoxCollider2D> boxList, List<ColliderTrigger> triggerList, GMUpdateCollider.Axial axial, int instanceID)
    {
        string name = axial == GMUpdateCollider.Axial.AxialXY ? "collider_XY" : "collider_Z";
        Transform parent = axial == GMUpdateCollider.Axial.AxialXY ? m_collidersXY_parent : m_collidersZ_parent;
        GameObject newCollider = new GameObject(name);
        BoxCollider2D newColl = newCollider.AddComponent<BoxCollider2D>();
        ColliderTrigger newCollTrigger = newCollider.AddComponent<ColliderTrigger>();
        newCollTrigger.entity = this;
        newCollTrigger.axial = axial;
        newCollTrigger.axialInstanceID = instanceID;
        triggerList.Add(newCollTrigger);
        newCollider.transform.SetParent(parent, false);
        boxList.Add(newColl);
    }
}
