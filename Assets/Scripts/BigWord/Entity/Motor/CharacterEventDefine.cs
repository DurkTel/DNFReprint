﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum CharacterEventDefine
//{
//    NONE,
//    ADD_JUMP_FORCE,
//    AIR_ATTACK_COMBO,
//    SKILL_COMBO,
//    MOVE_COEFFICIENT_CHANGE,
//    ADD_MOVE_FORCE,
//}

/// <summary>
/// 自定义条件类型 在此声明枚举 用于动画机自动切换动画
/// </summary>
public enum CustomCondition
{ 
    NONE,
    NO_MOVE_INPUT,
    RUN_LIMIT,
    WALK_LIMIT,
    JUMP_ATTACK_LIMIT,
    HIT_RECOVER,
    NO_MOVE
}


public enum EventParamDefine
{ 
    None,
    Bool,
    Int,
    Float,
    String,
}

public enum InputActionDefine
{
    Up,
    Down,
    Left,
    Right,
    Jump,
    Attack_1,
    Attack_2,
    Skill_1,
    Skill_2,
    Skill_3,
    Skill_4,

}
