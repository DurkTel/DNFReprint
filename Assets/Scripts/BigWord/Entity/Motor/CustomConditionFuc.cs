using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomConditionFuc
{
    public static bool IsStudiedThisSkill(int skillCode, SkillTree skillTree)
    {
        return skillTree.IsHasSkill(skillCode);
    }
}
