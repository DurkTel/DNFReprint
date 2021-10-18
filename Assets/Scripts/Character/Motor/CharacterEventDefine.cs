using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterEventDefine
{
    NONE,
    ADD_JUMP_FORCE,
    AIR_ATTACK_COMBO,
    SKILL_COMBO,
}

/// <summary>
/// 自定义条件类型 在此声明枚举
/// </summary>
public enum CustomCondition
{ 
    NONE,
    JUMP_ATTACK_LIMIT,
}


public enum CharacterEventParamDefine
{ 
    Bool,
    Int,
    Float,
    String,
}
