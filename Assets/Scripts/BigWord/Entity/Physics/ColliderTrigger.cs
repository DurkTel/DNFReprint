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
    /// 碰撞信息的实体ID（XYZ）
    /// </summary>
    public int axialInstanceID;
    /// <summary>
    /// 当前轴向接触的碰撞信息
    /// </summary>
    public List<ColliderTrigger> contectTrigger = new List<ColliderTrigger>();

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (entity != null && collision.TryGetComponent(out ColliderTrigger trigger))
        {
            if (trigger.axial == axial && !contectTrigger.Contains(trigger))
            {
                contectTrigger.Add(trigger);
            }
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
