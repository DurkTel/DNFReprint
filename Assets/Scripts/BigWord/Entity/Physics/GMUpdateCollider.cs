using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GMUpdateCollider : MonoBehaviour
{
    public enum Axial
    {
        AxialXY = 0,
        AxialZ = 1,
    }
    public interface IColliderInfo
    {
        bool updateColliderEnabled { get; set; }
        GMUpdateCollider colliderUpdate { get; set; }
        ColliderInfos own_colliderInfo { get; set; }
        List<ColliderTrigger> triggerZ { get; }
        List<ColliderTrigger> triggerXY { get; }
        List<ColliderInfos> contact_colliderInfos { get; }
        void OnGMUpdateColliderStayOut(bool stayOut, Entity entity);

    }

    private List<IColliderInfo> m_allColliderInfo = new List<IColliderInfo>();
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
                        bool isContentXYZ = contectXY.All(b => contectZ.Any(a => a.axialInstanceID == b.axialInstanceID));
                        if (isContentXYZ)
                        {
                            self.OnGMUpdateColliderStayOut(true, self.triggerXY[i].entity);
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
