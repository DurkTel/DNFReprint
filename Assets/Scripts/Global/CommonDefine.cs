using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonDefine
{
    public enum Career
    {
        Swordsman = 1,
        Shooter = 2,
        Fighter = 3,
    }

    public const string AnimationDataAssetPath = "Assets/ScriptableObjects/AnimationData/";

    /// <summary>
    /// 重力加速度
    /// </summary>
    public const float GravitationalAcceleration = 9.8f;
}
