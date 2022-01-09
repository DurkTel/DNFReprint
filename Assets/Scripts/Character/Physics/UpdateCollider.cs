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

    private Dictionary<int, AxialInfo> m_collIderDic = new Dictionary<int, AxialInfo>();

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

    private void FixedUpdate()
    {
        RefreshColliderInfo();
    }

    private void AddUpdateEvent()
    {
        m_spriteAnimator.UpdateAnimationEvent += InitCollider;

        m_spriteAnimator.UpdateSpriteEvent += RefreshCollider;
    }

    private void InitCollider(AnimationData animationData)
    {
        m_animationData = animationData;
        if (m_animationData.colliderInfo == null) return;
        m_colliderInfo = m_animationData.colliderInfo;

        int maxCout = 0;
        if (m_colliderInfo != null)
        {
            foreach (var frame in m_colliderInfo.frameCollInfos)
            {
                if (maxCout < frame.single_colliderInfo.Count)
                    maxCout = frame.single_colliderInfo.Count;
            }
        }

        int instanceID = this.GetInstanceID();

        //根据该动画的最大碰撞盒数创建碰撞盒
        if (m_collidersXY.Count < maxCout)
        {
            int length = maxCout - m_collidersXY.Count;
            for (int i = 0; i < length; i++)
            {
                GameObject newColliderXY = new GameObject("collider_XY");
                BoxCollider2D newCollXY = newColliderXY.AddComponent<BoxCollider2D>();
                ColliderTrigger newCollTriggerXY = newColliderXY.AddComponent<ColliderTrigger>();
                newCollTriggerXY.updateCollider = this;
                newCollTriggerXY.axial = Axial.AxialXY;
                newCollTriggerXY.axialInstanceID = instanceID;
                newColliderXY.transform.SetParent(m_collidersXY_parent, false);
                m_collidersXY.Add(newCollXY);

                GameObject newColliderZ = new GameObject("collider_Z");
                BoxCollider2D newCollZ = newColliderZ.AddComponent<BoxCollider2D>();
                ColliderTrigger newCollTriggerZ = newColliderZ.AddComponent<ColliderTrigger>();
                newCollTriggerZ.updateCollider = this;
                newCollTriggerZ.axial = Axial.AxialZ;
                newCollTriggerZ.axialInstanceID = instanceID;
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

            for (int i = 0; i < m_collidersXY.Count; i++)
            {
                bool isActive = maxCout - 1 >= i;
                m_collidersXY[i].gameObject.SetActive(isActive);
                m_collidersZ[i].gameObject.SetActive(isActive);
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
        if (currentFrame >= totalCollFrame) return;

        m_frameCollInfo = m_colliderInfo.frameCollInfos[currentFrame];

        int isFilp = m_renenderSprite.GetCurFlip();

        for (int i = 0; i < m_frameCollInfo.single_colliderInfo.Count; i++)
        {
            m_collidersXY[i].offset = m_frameCollInfo.single_colliderInfo[i].offset;
            m_collidersXY[i].size = m_frameCollInfo.single_colliderInfo[i].size;
            m_collidersXY[i].isTrigger = m_frameCollInfo.single_colliderInfo[i].isTrigger;
            m_collidersXY[i].gameObject.layer = (int)m_frameCollInfo.single_colliderInfo[i].layer + 9;
            m_collidersXY[i].transform.localScale = new Vector3(isFilp, 1, 1);

            m_collidersZ[i].offset = new Vector2(m_frameCollInfo.single_colliderInfo[i].offset.x, m_frameCollInfo.single_colliderInfo[i].offset_Z);
            m_collidersZ[i].size = new Vector2(m_frameCollInfo.single_colliderInfo[i].size.x, m_frameCollInfo.single_colliderInfo[i].size_Z);
            m_collidersZ[i].isTrigger = m_frameCollInfo.single_colliderInfo[i].isTrigger;
            m_collidersZ[i].gameObject.layer = (int)m_frameCollInfo.single_colliderInfo[i].layer + 9;
            m_collidersZ[i].transform.localScale = new Vector3(isFilp, 1, 1);

        }

    }

    private void RefreshColliderInfo()
    {
        foreach (var coll in m_collIderDic)
        {
            if (coll.Value.XY && coll.Value.Z)
            {
                //对方的碰撞层级
                switch (coll.Value.colllayer - 9)
                {
                    case ColliderLayer.Scene:
                        break;
                    case ColliderLayer.Interact:
                        break;
                    case ColliderLayer.Damage:
                        print(string.Format("ID:{0}受到来自ID:{1}的攻击！", this.GetInstanceID(), coll.Key));
                        break;
                    case ColliderLayer.BeDamage:
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void AddColliderInfo(int instanceID, Axial axial, ColliderLayer colliderLayer)
    {
        if (m_collIderDic.ContainsKey(instanceID))
        {
            if (m_collIderDic[instanceID].XY && m_collIderDic[instanceID].Z) return;
            AxialInfo newInfo = m_collIderDic[instanceID];
            newInfo.colllayer = colliderLayer;
            m_collIderDic[instanceID] = newInfo;
            switch (axial)
            {
                case Axial.AxialXY:
                    //结构体get为值传递而非地址
                    AxialInfo newInfoXY = m_collIderDic[instanceID];
                    newInfoXY.XY = true;
                    m_collIderDic[instanceID] = newInfoXY;
                    break;
                case Axial.AxialZ:
                    AxialInfo newInfoZ = m_collIderDic[instanceID];
                    newInfoZ.Z = true;
                    m_collIderDic[instanceID] = newInfoZ;
                    break;
                default:
                    break;
            }
        }
        else
        {
            AxialInfo newInfo = new AxialInfo(axial, colliderLayer);
            m_collIderDic.Add(instanceID, newInfo);
        }
    }

    public void RemoveColliderInfo(int instanceID, Axial axial)
    {
        if (m_collIderDic.ContainsKey(instanceID))
        {
            switch (axial)
            {
                case Axial.AxialXY:
                    //结构体get为值传递而非地址
                    AxialInfo newInfoXY = m_collIderDic[instanceID];
                    newInfoXY.XY = false;
                    m_collIderDic[instanceID] = newInfoXY;
                    break;
                case Axial.AxialZ:
                    AxialInfo newInfoZ = m_collIderDic[instanceID];
                    newInfoZ.Z = false;
                    m_collIderDic[instanceID] = newInfoZ;
                    break;
                default:
                    break;
            }
            //所有轴向的碰撞都不存在，移除
            if (!m_collIderDic[instanceID].XY && !m_collIderDic[instanceID].Z)
            {
                m_collIderDic.Remove(instanceID);
            }
        }
    }

    public virtual void OnDrawGizmos()
    {
        if (m_colliderInfo == null) return;

        int isFilp = m_renenderSprite.GetCurFlip();
        for (int i = 0; i < m_frameCollInfo.single_colliderInfo.Count; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y) + m_collidersXY[i].offset * 0.01f * new Vector2(isFilp, 1), m_collidersXY[i].size * 0.01f);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y) + m_collidersZ[i].offset * 0.01f * new Vector2(isFilp, 1), m_collidersZ[i].size * 0.01f);

        }
    }

    public enum Axial
    { 
        AxialXY = 0,
        AxialZ = 1,
    }

    public struct AxialInfo
    {
        public bool XY;
        public bool Z;
        public ColliderLayer colllayer;

        public AxialInfo(Axial axial, ColliderLayer layer)
        {
            switch (axial)
            {
                case Axial.AxialXY:
                    XY = true;
                    Z = false;
                    break;
                case Axial.AxialZ:
                    XY = false;
                    Z = true;
                    break;
                default:
                    XY = false;
                    Z = false;
                    break;
            }
            colllayer = layer;
        }
    }
}
