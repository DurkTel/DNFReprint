using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonUtility
{
    public enum Career
    {
        None = 0,
        Swordsman = 1,
        Shooter = 2,
        Fighter = 3,
    }
    public const string AnimationDataAssetPath = "Assets/ScriptableObjects/AnimationData/";
    /// <summary>
    /// 重力加速度
    /// </summary>
    public const float GravitationalAcceleration = 9.8f;
    /// <summary>
    /// 空气阻力
    /// </summary>
    public const float AirFriction = 1f;
    /// <summary>
    /// 顿帧强度
    /// </summary>
    public const float HardStraight = 1f;
    /// <summary>
    /// 顿帧最大值
    /// </summary>
    public const float HaltFrameMax = 100000;
}
