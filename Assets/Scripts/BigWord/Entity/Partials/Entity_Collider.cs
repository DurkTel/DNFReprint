using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private List<ColliderTrigger> m_triggerZ = new List<ColliderTrigger>();
    /// <summary>
    /// XY轴触发器列表
    /// </summary>
    private List<ColliderTrigger> m_triggerXY = new List<ColliderTrigger>();
    /// <summary>
    /// XY轴碰撞盒列表
    /// </summary>
    private List<BoxCollider2D> m_collidersXY = new List<BoxCollider2D>();
    /// <summary>
    /// Z轴碰撞盒列表
    /// </summary>
    private List<BoxCollider2D> m_collidersZ = new List<BoxCollider2D>();
    /// <summary>
    /// 受击碰撞计数
    /// </summary>
    private Dictionary<int, ContentDamageHit> m_hitCount = new Dictionary<int, ContentDamageHit>();
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
    public int hurt { get; set; }

    public void OnGMUpdateColliderStayOut(bool inOut, Entity entity, ColliderInfos collInfo, int layer)
    {
        int newlayer = layer - 9;
        ColliderLayer layerEnum = (ColliderLayer)newlayer;
        switch (layerEnum)
        {
            case ColliderLayer.Scene:
                break;
            case ColliderLayer.Interact:
                break;
            case ColliderLayer.Damage:
                ContentDamageHandler(inOut, entity, collInfo);
                break;
            case ColliderLayer.BeDamage:
                break;
            default:
                break;
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
                m_triggerXY[i].hashCode = frameCollInfo.single_colliderInfo[i].GetHashCode();

                m_collidersZ[i].offset = new Vector2(info.offset.x, info.offset_Z);
                m_collidersZ[i].size = new Vector2(info.size.x, info.size_Z);
                m_collidersZ[i].isTrigger = info.isTrigger;
                m_collidersZ[i].gameObject.layer = (int)info.layer + 9;
                m_collidersZ[i].transform.localScale = collScale;
                m_triggerZ[i].hashCode = frameCollInfo.single_colliderInfo[i].GetHashCode();

            }
            m_collidersXY[i].enabled = isActive;
            m_collidersZ[i].enabled = isActive;
        }

    }

    private void CreateCollider(List<BoxCollider2D> boxList, List<ColliderTrigger> triggerList, GMUpdateCollider.Axial axial, ColliderInfos colliderInfos)
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


    private void ContentDamageHandler(bool inOut, Entity entity, ColliderInfos collInfo)
    {
        int hashCode = collInfo.GetHashCode();
        EntitySkill entitySkill = SkillConfig.GetInfoByCode(collInfo.skillCode);
        if (inOut)
        {
            //限制攻击段数 攻击间隔
            if (!m_hitCount.ContainsKey(hashCode) || (m_hitCount[hashCode].hitCount < entitySkill.NumbeOfAttacks && Time.realtimeSinceStartup - m_hitCount[hashCode].lastHitTime >= entitySkill.NumbeOfInterval))
            {
                Debug.LogFormat("造成伤害！！！伤害来源实体是：{0}", collInfo.name);
                entitySkill.hurtObj = entity.gameObject;
                if (entitySkill != null)
                {
                    DamageData data = DamageConfig.GetInfoByCode(entitySkill.DamageCode);
                    data.attacker = entity.gameObject;
                    MoveHurt_OnStart(data);
                }


                if (m_hitCount.ContainsKey(hashCode))
                {
                    int curHit = m_hitCount[hashCode].hitCount;
                    m_hitCount[hashCode] = new ContentDamageHit() { hitCount = curHit + 1, lastHitTime = Time.realtimeSinceStartup };
                }
                else
                {
                    m_hitCount.Add(hashCode, new ContentDamageHit() { hitCount = 1, lastHitTime = Time.realtimeSinceStartup });
                }
            }
        }
        else
        {
            m_hitCount.Remove(hashCode);
        }
    }

    public struct ContentDamageHit
    {
        public int hitCount;

        public float lastHitTime;
    }
}
