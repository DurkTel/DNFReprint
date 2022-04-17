using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillConfig
{
    private static Dictionary<int, EntitySkill> m_skillDic = new Dictionary<int, EntitySkill>();

    private static void GetInfo()
    {
        EntitySkill[] skillInfos = JsonManager.LoadConfig<EntitySkill[]>("t_skill");

        foreach (var item in skillInfos)
        {
            m_skillDic.Add(item.SkillCode, item);
        }
    }


    public static EntitySkill GetInfoByCode(int skillCode)
    {
        if (skillCode == 0) return null;
        if (!m_skillDic.ContainsKey(skillCode))
            GetInfo();

        if (!m_skillDic.ContainsKey(skillCode))
        {
            Debug.LogError("t_skill配置表中没有该code" + skillCode);
            return null;
        }

        return m_skillDic[skillCode];
    }
}
