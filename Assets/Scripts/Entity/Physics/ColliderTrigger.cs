using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    [HideInInspector]
    public UpdateCollider updateCollider;

    public Axial axial;

    public int axialInstanceID;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (updateCollider == null) return;
        if (collision.TryGetComponent(out ColliderTrigger trigger))
        {
            if (trigger.axial == axial)
            {
                updateCollider.AddColliderInfo(trigger.axialInstanceID, axial, collision, trigger.updateCollider.colliderInfo);
            }
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (updateCollider == null) return;
    //    if (collision.TryGetComponent(out ColliderTrigger trigger))
    //    {
    //        if (trigger.axial == axial)
    //        {
    //            updateCollider.AddColliderInfo(trigger.axialInstanceID, axial, (ColliderLayer)collision.gameObject.layer, trigger.updateCollider.colliderInfo);
    //        }
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (updateCollider == null) return;
        if (collision.TryGetComponent(out ColliderTrigger trigger))
        {
            if (trigger.axial == axial)
            {
                updateCollider.RemoveColliderInfo(trigger.axialInstanceID, axial);
            }
        }
    }
}
