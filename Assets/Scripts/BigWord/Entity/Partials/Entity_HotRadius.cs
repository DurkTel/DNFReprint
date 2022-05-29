using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Entity
{
    public int maxHotRadiusNum = 3;
    public float[] hotRadius { get; private set; }

    public void Set_HotRadius(int hotIndex, float radius)
    {
        if (hotIndex < 0 || hotIndex > 3)
            return;

        if (hotRadius == null)
            hotRadius = new float[maxHotRadiusNum];

        hotRadius[hotIndex] = radius;
    }

    private void ReleaseHotRadius()
    {
        if (hotRadius != null)
        {
            for (int i = 0; i < hotRadius.Length; i++)
            {
                hotRadius[i] = 0f;
            }
        }
    }
}
