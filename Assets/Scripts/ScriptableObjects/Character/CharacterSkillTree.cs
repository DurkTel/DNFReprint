using cfg.db;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName ="SkillTree", menuName = "ScriptableObject/Skill/SkillTree")]
public class SkillTree
{
    private Dictionary<int, SkillCfg> m_skillTree = new Dictionary<int, SkillCfg>();
    public void AddSkill(int skillCode)
    {
        SkillCfg skill = MDefine.tables.TbSkill.Get(skillCode);
        if (skill == null) return;

        bool conditionSkill = skill.Condition == 0 ? true : m_skillTree.ContainsKey(skill.Condition);
        bool isStudy = m_skillTree.ContainsKey(skill.Id);
        if (!isStudy && conditionSkill)
        {
            m_skillTree.Add(skill.Id, skill);
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

    public SkillCfg GetSkill(int skillCode)
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
