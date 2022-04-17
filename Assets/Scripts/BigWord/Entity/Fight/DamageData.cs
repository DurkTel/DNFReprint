using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageData
{
    /// <summary>
    /// 伤害
    /// </summary>
    public int hurt;
    /// <summary>
    /// 攻击者
    /// </summary>
    public GameObject attacker;
    /// <summary>
    /// 面朝攻击者
    /// </summary>
    public bool lookAttacker;
    /// <summary>
    /// 加速度
    /// </summary>
    public float acceleration;
    /// <summary>
    /// 对X轴施加的初速度
    /// </summary>
    public float velocityX;
    /// <summary>
    /// 浮空的高度
    /// </summary>
    public float heightY;
    /// <summary>
    /// 在空中时对X轴施加的初速度
    /// </summary>
    public float velocityXY;
    /// <summary>
    /// 硬直时间
    /// </summary>
    public float RecoverTime;
}
