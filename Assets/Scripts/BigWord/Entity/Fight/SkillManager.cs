using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using cfg.db;

/// <summary>
/// 管理攻击或释放技能
/// </summary>
public class SkillManager
{
    private Entity m_entity;

    private Dictionary<InputActionDefine, int> m_actionSkillCodeMap = new Dictionary<InputActionDefine, int>();

    private Dictionary<SkillCfg, float> m_skillCoolingMap = new Dictionary<SkillCfg, float>();

    public SkillTree characterSkillTree;

    public InputReader inputReader;

    private void OnDisable()
    {
        inputReader.buttonPressEvent -= SkillAction;
    }

    public SkillManager(Entity entity)
    {
        m_entity = entity;
        characterSkillTree = new SkillTree();
    }

    public void Init()
    {
        inputReader.buttonPressEvent += SkillAction;

        //先测试
        characterSkillTree.AddSkill(10002);
        characterSkillTree.AddSkill(10003);
        AddAcionSkill(InputActionDefine.Attack_2, 10003);
    }

    public void AddAcionSkill(InputActionDefine action, int code)
    {
        if (m_actionSkillCodeMap.ContainsKey(action))
        {
            m_actionSkillCodeMap[action] = code;
        }
        else
            m_actionSkillCodeMap.Add(action, code);

    }

    private void SkillAction(string actionName)
    {
        if (m_entity.status == EntityUnitily.FIGHT)
            return;
        InputActionDefine inputAction = CharacterEventFunc.GetInputStringToEnum(actionName);
        if (m_actionSkillCodeMap.ContainsKey(inputAction))
        {
            int skillCode = m_actionSkillCodeMap[inputAction];
            SkillCfg skill = characterSkillTree.GetSkill(skillCode);
            if (skill == null)
            {
                Debug.LogError(string.Format("技能code{0}在技能树中找不到对应的技能！", skillCode));
                return;
            }
            if (CanReleaseSkill(skill) && CheckSkillCD(skill) && !string.IsNullOrEmpty(skill.AnimationDataName))
            {
                m_entity.ForceDOSkillAnimation(skill.AnimationDataName, m_entity.careerType);
                
            }
        }
        else
            Debug.LogWarning(string.Format("按键Aciont{0}没有绑定技能", inputAction));
    }

    /// <summary>
    /// 技能是否冷却
    /// </summary>
    /// <param name="skill"></param>
    private bool CheckSkillCD(SkillCfg skill)
    {
        if (m_skillCoolingMap.ContainsKey(skill))
        {
            if (Time.time - m_skillCoolingMap[skill] >= skill.CD)
            {
                m_skillCoolingMap[skill] = Time.time;
                return true;
            }
            MusicManager.Instance.PlaySound("sm_cooltime");
            return false;
        }
        else
        {
            m_skillCoolingMap.Add(skill, Time.time);
            return true;
        }
    }

    public bool CanReleaseSkill(SkillCfg skill)
    {
        //先测试
        if (true)
        {
            return true;
        }

        return false;
    }
}
