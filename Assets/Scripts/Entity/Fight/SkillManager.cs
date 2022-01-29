using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private EntityMotor m_entityMotor;

    private SpriteAnimator m_spriteAnimator;

    private CharacterSkillTree m_characterSkillTree;

    void Start()
    {
        m_spriteAnimator = GetComponentInChildren<SpriteAnimator>();
        m_entityMotor = GetComponent<EntityMotor>();
        m_characterSkillTree = GetComponent<CharacterSkillTree>();
    }

    void Update()
    {
        TickEntitySkill();
    }

    public void TickEntitySkill()
    {
        if (KeyboardInput.Instance.ButtonFire2.OnPressed)
        {
            int skillCode = 10002;
            EntitySkill skill = m_characterSkillTree.GetSkill(skillCode);
            if (skill == null)
            {
                Debug.LogError(string.Format("技能code{0}在技能树中找不到对应的技能！", skillCode));
                return;
            }
            if (CanReleaseSkill(skill) && skill.animationData != null)
            {
                m_spriteAnimator.ForceDOSkillAnimation(skill.animationData);
            }
        }
    }

    public bool CanReleaseSkill(EntitySkill skill)
    {
        //先测试
        if (true)
        {
            return true;
        }

        return false;
    }
}
