using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Animation/ColliderInfo")]
public class ColliderInfo : ScriptableObject
{
    public List<FrameCollInfo> frameCollInfos = new List<FrameCollInfo>();
}

[System.Serializable]
public struct FrameCollInfo
{
    public List<CollValueConfig> collValueConfigs;
}

[System.Serializable]
public struct CollValueConfig
{ 
    public ColliderLayer layer;

    public bool isTrigger;

    public float offset_Z;

    public float size_Z;

    public Vector2 offset;

    public Vector2 size;
    
}

public enum ColliderLayer
{
    Scene = 9,
    Interact = 10,
    Damage = 11,
    BeDamage = 12,
}
