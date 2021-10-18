using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillTree
{
    private Dictionary<int, CharacterSkill> skillTree = new Dictionary<int, CharacterSkill>();
    public void AddSkill(CharacterSkill skill)
    {
        if (!skillTree.ContainsKey(skill.SkillCode))
        {
            skillTree.Add(skill.SkillCode, skill);
        }
    }

    public void RemoveSkill(int skillCode)
    {
        if (skillTree.ContainsKey(skillCode))
        {
            skillTree.Remove(skillCode);
        }
    }

    public CharacterSkill GetSkill(int skillCode)
    {
        if (skillTree.ContainsKey(skillCode))
        {
            return skillTree[skillCode];
        }

        return null;
    }

    public bool IsHasSkill(int skillCode)
    {
        return skillTree.ContainsKey(skillCode);
    }
}
