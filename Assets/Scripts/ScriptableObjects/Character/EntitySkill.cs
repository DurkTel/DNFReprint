﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySkill : IDamage
{
    public enum CharacterSkillType
    {
        Damage,
        Buff,
        Attribute,
        Functionality
    }

    /// <summary>
    /// 技能类型
    /// </summary>
    public CharacterSkillType SkillType;

    /// <summary>
    /// 用于区分同一技能不同等级
    /// </summary>
    public int SameID;

    /// <summary>
    /// 技能code
    /// </summary>
    public int SkillCode;

    /// <summary>
    /// 技能名称
    /// </summary>
    public string SkillName;

    /// <summary>
    /// 技能等级
    /// </summary>
    public int SkillLevel;

    /// <summary>
    /// 攻击段数
    /// </summary>
    public int NumbeOfAttacks;

    /// <summary>
    /// 每段的间隔时间
    /// </summary>
    public float NumbeOfInterval;

    /// <summary>
    /// 每段的伤害
    /// </summary>
    public float Damage;

    /// <summary>
    /// 冷却时间
    /// </summary>
    public float CD;

    /// <summary>
    /// 条件限制
    /// </summary>
    public int Condition;

    /// <summary>
    /// 开启等级
    /// </summary>
    public int OpenLevel;

    /// <summary>
    /// 所需魔法
    /// </summary>
    public float NeedMP;

    /// <summary>
    /// 所需生命
    /// </summary>
    public float NeedHP;

    /// <summary>
    /// 浮空值
    /// </summary>
    public float AirBorneForce;

    /// <summary>
    /// 是否能浮空
    /// </summary>
    public bool CanAirBorne;

    /// <summary>
    /// 表现动画
    /// </summary>
    public string AnimationDataName;

    /// <summary>
    /// 伤害code
    /// </summary>
    public int DamageCode;

    /// <summary>
    /// 技能归属对象 
    /// </summary>
    private GameObject m_hurtObj;
    public float hurt { get => Damage; }
    public GameObject hurtObj { get => m_hurtObj; set => m_hurtObj = value; }
    public Transform hurtTransform { get => m_hurtObj.transform; }
}
