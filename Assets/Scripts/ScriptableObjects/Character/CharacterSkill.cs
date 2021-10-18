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
        Functionality
    }

    public int SkillCode;

    public string SkillName;

    public CharacterSkillType SkillType;

    public float CD;

    public float NeedMP;

}
