using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName ="SkillTree", menuName = "ScriptableObject/Skill/SkillTree")]
public class SkillTree
{
    private Dictionary<int, EntitySkill> m_skillTree = new Dictionary<int, EntitySkill>();
    public void AddSkill(int skillCode)
    {
        EntitySkill skill = SkillConfig.GetInfoByCode(skillCode);
        if (skill == null) return;

        bool conditionSkill = skill.Condition == 0 ? true : m_skillTree.ContainsKey(skill.Condition);
        bool isStudy = m_skillTree.ContainsKey(skill.SkillCode);
        if (!isStudy && conditionSkill)
        {
            m_skillTree.Add(skill.SkillCode, skill);
        }
        else
        {
            if (isStudy)
            {
                Debug.LogError("当前技能已学习");
            }
            else
            {
                Debug.LogError("前置技能没有学习");
            }
        }
    }

    //public void RemoveSkill(int skillCode)
    //{
    //    if (skillTree.ContainsKey(skillCode))
    //    {
    //        skillTree.Remove(skillCode);
    //    }
    //}

    public EntitySkill GetSkill(int skillCode)
    {
        if (m_skillTree.ContainsKey(skillCode))
        {
            return m_skillTree[skillCode];
        }

        return null;
    }

    public bool IsHasSkill(int skillCode)
    {
        return m_skillTree.ContainsKey(skillCode);
    }

    public void ClearSkillTree()
    {
        m_skillTree.Clear();
    }
}
