using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GMUpdateCollider : MonoBehaviour
{
    /// <summary>
    /// 碰撞轴向 XY轴 或 Z轴
    /// </summary>
    public enum Axial
    {
        AxialXY = 0,
        AxialZ = 1,
    }

    /// <summary>
    /// 两个连接的碰撞
    /// </summary>
    public struct ContactPair
    {
        public ColliderTrigger attacker;
        public ColliderTrigger victim;
    }
    public interface IColliderInfo
    {
        /// <summary>
        /// 启动碰撞检测
        /// </summary>
        bool updateColliderEnabled { get; set; }
        /// <summary>
        /// 碰撞管理实例
        /// </summary>
        GMUpdateCollider colliderUpdate { get; set; }
        /// <summary>
        /// 继承接口的实体自身的碰撞信息
        /// </summary>
        ColliderInfos own_colliderInfo { get; set; }
        /// <summary>
        /// Z轴所有的碰撞信息
        /// </summary>
        List<ColliderTrigger> triggerZ { get; }
        /// <summary>
        /// XY轴所有的碰撞信息
        /// </summary>
        List<ColliderTrigger> triggerXY { get; }
        /// <summary>
        /// XYZ轴碰撞或离开时的回调
        /// </summary>
        /// <param name="stayOut">碰撞或离开</param>
        /// <param name="collInfo">与自己发生碰撞的信息</param>
        void ContactHandle(ContactPair contact, ColliderInfos collInfo);

    }

    private Dictionary<int, IColliderInfo> m_allColliderInfo = new Dictionary<int, IColliderInfo>();

    private Dictionary<int, List<ContactPair>> m_contactDic = new Dictionary<int, List<ContactPair>>();

    private Dictionary<int, List<int>> m_contectZ = new Dictionary<int, List<int>>();


    /// <summary>
    /// 添加需要碰撞检测的实体
    /// </summary>
    /// <param name="colliderObject">实体</param>
    public void AddColliderObject(int entityId, IColliderInfo colliderObject)
    {
        if (m_allColliderInfo.ContainsKey(entityId))
        {
            Debug.LogErrorFormat("已经添加过该碰撞信息{0}", colliderObject.own_colliderInfo.name);
            return;
        }

        m_allColliderInfo.Add(entityId,colliderObject);
        colliderObject.colliderUpdate = this;
    }
    /// <summary>
    /// 移除需要碰撞检测的实体
    /// </summary>
    /// <param name="colliderObject">实体</param>
    public void RemoveColliderObject(int entityId, IColliderInfo colliderObject)
    {
        if (!m_allColliderInfo.ContainsKey(entityId))
        {
            Debug.LogErrorFormat("没有添加过该碰撞信息{0}", colliderObject.own_colliderInfo.name);
            return;
        }

        colliderObject.colliderUpdate = null;
        m_allColliderInfo.Remove(entityId);
    }

    /// <summary>
    /// 检测是否存在XYZ碰撞
    /// </summary>
    public void UpdateColliderContent()
    {
        foreach (var contacts in m_contactDic.Values)
        {
            for (int i = 0; i < contacts.Count; i++)
            {
                if (contacts[i].attacker.axial == Axial.AxialXY && contacts[i].victim.axial == Axial.AxialXY)
                {
                    int id = contacts[i].attacker.entity.entityId;
                    if (m_contectZ.TryGetValue(id, out List<int> zList) && zList.Contains(contacts[i].victim.hashCode))
                    {
                        //攻击者  =======》  被击者
                        m_allColliderInfo[id].ContactHandle(contacts[i], m_allColliderInfo[id].own_colliderInfo);
                    }
                }
            }
            contacts.Clear();
        }
    }

    public void AddContact(int entityId, ColliderTrigger own, ColliderTrigger other)
    {
        if (!m_allColliderInfo.ContainsKey(entityId) || !m_allColliderInfo[entityId].updateColliderEnabled) return;
        if (m_contactDic.ContainsKey(entityId))
        {
            m_contactDic[entityId].TryUniqueAdd(new ContactPair { attacker = own, victim = other });
        }
        else
        {
            m_contactDic.Add(entityId, new List<ContactPair> { new ContactPair { attacker = own, victim = other } });
        }
    }

    public void AddContact(int entityId, int infoId)
    {
        if (!m_allColliderInfo.ContainsKey(entityId) || !m_allColliderInfo[entityId].updateColliderEnabled) return;
        if (m_contectZ.ContainsKey(entityId))
        {
            m_contectZ[entityId].TryUniqueAdd(infoId);
        }
        else
        {
            m_contectZ.Add(entityId, new List<int> { infoId });
        }
    }

    public void ClearContact(int entityId)
    {
        if (m_contactDic.ContainsKey(entityId))
        {
            m_contactDic[entityId].Clear();
        }

        if (m_contectZ.ContainsKey(entityId))
        {
            m_contectZ[entityId].Clear();
        }
    }

    public void ClearContactZ(int entityId)
    {
        if (m_contectZ.ContainsKey(entityId))
        {
            m_contectZ[entityId].Clear();
        }
    }

    /// <summary>
    /// 有碰撞离开
    /// </summary>
    //public void ExitColliderContent(IColliderInfo info ,ColliderTrigger trigger)
    //{
    //    if (m_contectDict.ContainsKey(info) && m_contectDict[info].Contains(trigger))
    //    {
    //        m_contectDict[info].Remove(trigger);
    //        info.OnGMUpdateColliderStayOut(false, trigger.entity, trigger.colliderInfos, trigger.layer);
    //        if (m_contectDict[info].Count <= 0)
    //        { 
    //            m_contectDict.Remove(info);
    //        }
    //    }

    //}

    public virtual void OnDrawGizmos()
    {
        foreach (Entity entity in m_allColliderInfo.Values)
        {
            if (entity.own_colliderInfo == null || entity.frameCollInfo.single_colliderInfo == null || entity.frameCollInfo.single_colliderInfo.Count == 0) return;

            int isFilp = entity.curFlip;
            for (int i = 0; i < entity.frameCollInfo.single_colliderInfo.Count; i++)
            {
                try
                {
                    if (entity != null && entity.collidersXY != null && entity.collidersZ != null && entity.collidersXY.Count > 0 && entity.collidersZ.Count > 0)
                    {
                        Gizmos.color = Color.green;
                        Gizmos.DrawWireCube(new Vector2(entity.transform.position.x, entity.collidersXY_parent.position.y) + entity.collidersXY[i].offset * 0.01f * new Vector2(isFilp, 1), entity.collidersXY[i].size * 0.01f);
                        Gizmos.color = Color.yellow;
                        Gizmos.DrawWireCube(new Vector2(entity.transform.position.x, entity.collidersZ_parent.position.y) + entity.collidersZ[i].offset * 0.01f * new Vector2(isFilp, 1), entity.collidersZ[i].size * 0.01f);
                    }

                }
                catch(Exception e)
                {
                    print(e);
                }
            }
        }
    }
}
