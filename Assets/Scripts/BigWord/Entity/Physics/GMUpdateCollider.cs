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
        void OnGMUpdateColliderStayOut(bool stayOut, GameObject obj, ColliderInfos collInfo);

    }

    private List<IColliderInfo> m_allColliderInfo = new List<IColliderInfo>();
    /// <summary>
    /// 添加需要碰撞检测的实体
    /// </summary>
    /// <param name="colliderObject">实体</param>
    public void AddColliderObject(IColliderInfo colliderObject)
    {
        if (m_allColliderInfo.Contains(colliderObject))
        {
            Debug.LogErrorFormat("已经添加过该碰撞信息{0}", colliderObject.own_colliderInfo.name);
            return;
        }

        m_allColliderInfo.Add(colliderObject);
        colliderObject.colliderUpdate = this;
    }
    /// <summary>
    /// 移除需要碰撞检测的实体
    /// </summary>
    /// <param name="colliderObject">实体</param>
    public void RemoveColliderObject(IColliderInfo colliderObject)
    {
        if (!m_allColliderInfo.Contains(colliderObject))
        {
            Debug.LogErrorFormat("没有添加过该碰撞信息{0}", colliderObject.own_colliderInfo.name);
            return;
        }

        m_allColliderInfo.Remove(colliderObject);
        colliderObject.colliderUpdate = null;
    }

    /// <summary>
    /// 检测是否存在XYZ碰撞
    /// </summary>
    public void UpdateColliderContent()
    {
        foreach (var self in m_allColliderInfo)
        {
            if (self.updateColliderEnabled)
            {
                for (int i = 0; i < self.triggerXY.Count; i++)
                {
                    List<ColliderTrigger> contectXY = self.triggerXY[i].contectTrigger;
                    List<ColliderTrigger> contectZ = self.triggerZ[i].contectTrigger;
                    if (contectXY.Count > 0 && contectZ.Count > 0)
                    {
                        GameObject obj = null;
                        ColliderInfos collInfo = null;
                        //XY轴列表和Z轴列表中存在相同的碰撞信息实例id 意味着那个碰撞在XYZ轴方向上都成立
                        bool isContentXYZ = contectXY.All(b => contectZ.Any(a => 
                        {
                            if (a.hashCode == b.hashCode)
                            {
                                collInfo = a.colliderInfos;
                                obj = a.gameObject;
                                return true;
                            }

                            return false;
                        }));
                        if (isContentXYZ)
                        {
                            self.OnGMUpdateColliderStayOut(true, obj, collInfo);
                            //return;
                        }
                    }
                }
            }
        }
    }

    public virtual void OnDrawGizmos()
    {
        foreach (Entity entity in m_allColliderInfo)
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
