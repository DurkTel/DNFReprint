using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一个动画的碰撞信息
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/Animation/ColliderInfo")]
public class ColliderInfos : ScriptableObject
{
    public List<FrameCollInfo> frameCollInfos = new List<FrameCollInfo>();

    public int frameCount;

    public int skillCode;
}

/// <summary>
/// 单个碰撞信息
/// </summary>
[System.Serializable]
public struct ColliderInfo
{
    public ColliderLayer layer;

    public bool isTrigger;

    public float offset_Z;

    public float size_Z;

    public Vector2 offset;

    public Vector2 size;

}

/// <summary>
/// 每帧的碰撞信息
/// </summary>
[System.Serializable]
public struct FrameCollInfo
{
    /// <summary>
    /// 单个碰撞信息
    /// </summary>
    public List<ColliderInfo> single_colliderInfo;

    /// <summary>
    /// 选择的单个碰撞的下标
    /// </summary>
    public int index;
}

public enum ColliderLayer
{
    Scene = 0,
    Interact = 1,
    Damage = 2,
    BeDamage = 3,
}
