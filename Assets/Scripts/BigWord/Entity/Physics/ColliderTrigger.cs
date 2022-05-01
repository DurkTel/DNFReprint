using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    /// <summary>
    /// 绑定的实体
    /// </summary>
    [HideInInspector]
    public Entity entity;
    /// <summary>
    /// 轴向 XY 或 Z
    /// </summary>
    public GMUpdateCollider.Axial axial;
    /// <summary>
    /// 碰撞盒的实体ID（XYZ）
    /// </summary>
    public int hashCode;
    /// <summary>
    /// 当前轴向接触的碰撞信息
    /// </summary>
    public List<ColliderTrigger> contectTrigger = new List<ColliderTrigger>();
    /// <summary>
    /// 碰撞信息
    /// </summary>
    public ColliderInfos colliderInfos;
    /// <summary>
    /// 碰撞层级
    /// </summary>
    public ColliderLayer layer;

    public BoxCollider2D collider2d;

    private GMUpdateCollider m_GMUpdateCollider;

    private void Start()
    {
        collider2d = GetComponent<BoxCollider2D>();
        m_GMUpdateCollider = GMEntityManager.entityUpdateCollider;
    }

    private ColliderTrigger GetTriggerFromCollider(Collider2D collision)
    {
        if (collision.TryGetComponent(out ColliderTrigger trigger))
        {
            if (trigger.axial == this.axial)
            {
                if (trigger.entity == entity) return null;

                if (layer == ColliderLayer.BeDamage) return null;

                if (trigger.layer == ColliderLayer.Damage) return null;

                if (trigger.axial == GMUpdateCollider.Axial.AxialZ)
                { 
                    m_GMUpdateCollider.AddContact(entity.entityId, trigger.hashCode);
                    return null;
                }

                return trigger;
            }
        }

        return null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        ColliderTrigger trigger = GetTriggerFromCollider(collision);

        if (trigger != null)
        {
            m_GMUpdateCollider.AddContact(entity.entityId, this, trigger);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (entity != null && collision.TryGetComponent(out ColliderTrigger trigger))
        {
            if (trigger.axial == axial && contectTrigger.Contains(trigger))
            {
                contectTrigger.Remove(trigger);
            }
        }
    }
}
