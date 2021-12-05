using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Character/CharacterSkill")]
public class CharacterSkill : ScriptableObject
{
    public enum CharacterSkillType
    {
        Damage,
        Buff,
        Attribute,
        Functionality
    }

    public int SkillCode;

    public string SkillName;

    public CharacterSkillType SkillType;

    public float CD;

    public int Condition;

    public int Level;

    public float NeedMP;
}
