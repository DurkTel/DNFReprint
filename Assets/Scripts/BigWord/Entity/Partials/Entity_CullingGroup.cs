using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Entity : GMCullingGroup.ICulling
{
    public bool cullingGroupEnabled { get; set; }
    public float cullingRadius { get; set; }
    public int cullingLod { get; private set; }
    public GMCullingGroup cullingGroup { get; set; }
    public bool cullingVisible { get; private set; }

    private void CullGroupInit()
    {
        cullingGroupEnabled = true;
        cullingRadius = 0.5f;
        if (cullingGroup != null)
        {
            cullingLod = cullingGroup.GetDistance(this);
            OnGMCullingVisible(cullingGroup.IsVisible(this));
        }
    }

    public void OnGMCullingDistance(int lod, int lodMax)
    {

    }

    public void OnGMCullingVisible(bool visible)
    {
        if (!cullingGroupEnabled) return;

        cullingVisible = visible;
        Skin_SetVisible(visible);
    }

    public void CullGroupUpdate(float deltaTime)
    {
        if (!cullingGroupEnabled)
            return;

        if (cullingGroup)
            cullingGroup.UpdateBoundingSphere(this, transform.position, cullingRadius);
    }

    private void ReleaseCullGroup()
    {
        cullingGroupEnabled = false;
        cullingVisible = false;
        cullingRadius = 0;
    }

}
