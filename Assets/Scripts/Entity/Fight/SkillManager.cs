using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

/// <summary>
/// 管理攻击或释放技能
/// </summary>
public class SkillManager : MonoBehaviour
{
    private SpriteAnimator m_spriteAnimator;

    private Dictionary<InputActionDefine, int> m_actionSkillCodeMap = new Dictionary<InputActionDefine, int>();

    private Dictionary<EntitySkill, float> m_skillCoolingMap = new Dictionary<EntitySkill, float>();

    public SkillTree characterSkillTree;

    public InputReader inputReader;

    //先写死测试
    public EntitySkill jumpAttack;

    public EntitySkill shangtiaoSkill;

    private void Reset()
    {
        if (characterSkillTree == null)
        {
            string path = string.Format("Assets/ScriptableObjects/Data/SkillTree/{0}/SkllTree_{1}.asset", this.gameObject.name, this.gameObject.name);
            if (File.Exists(path))
            {
                SkillTree skillTree = AssetDatabase.LoadAssetAtPath(path, typeof(SkillTree)) as SkillTree;
                characterSkillTree = skillTree;

            }
            else
            {
                Directory.CreateDirectory(path);
                var skillTree = ScriptableObject.CreateInstance<SkillTree>();
                AssetDatabase.CreateAsset(skillTree, path);
                characterSkillTree = skillTree;
            }
        }

        if (inputReader == null)
        {
            string path = "Assets/ScriptableObjects/Input/InputReader.asset";
            if (File.Exists(path))
            {
                InputReader input = AssetDatabase.LoadAssetAtPath(path, typeof(InputReader)) as InputReader;
                inputReader = input;

            }
            else
            {
                Directory.CreateDirectory(path);
                var input = ScriptableObject.CreateInstance<InputReader>();
                AssetDatabase.CreateAsset(input, path);
                inputReader = input;
            }
        }
    }

    private void OnEnable()
    {
        inputReader.buttonPressEvent += SkillAction;
    }

    private void OnDisable()
    {
        inputReader.buttonPressEvent -= SkillAction;

    }

    void Start()
    {
        m_spriteAnimator = GetComponentInChildren<SpriteAnimator>();

        //先测试
        characterSkillTree.AddSkill(jumpAttack);
        characterSkillTree.AddSkill(shangtiaoSkill);
        AddAcionSkill(InputActionDefine.Attack_2, 10002);
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
        InputActionDefine inputAction = CharacterEventFunc.GetInputStringToEnum(actionName);
        if (m_actionSkillCodeMap.ContainsKey(inputAction))
        {
            int skillCode = m_actionSkillCodeMap[inputAction];
            EntitySkill skill = characterSkillTree.GetSkill(skillCode);
            if (skill == null)
            {
                Debug.LogError(string.Format("技能code{0}在技能树中找不到对应的技能！", skillCode));
                return;
            }
            if (CanReleaseSkill(skill) && CheckSkillCD(skill) && skill.animationData != null)
            {
                m_spriteAnimator.ForceDOSkillAnimation(skill.animationData);
                
            }
        }
        else
            Debug.LogWarning(string.Format("按键Aciont{0}没有绑定技能", inputAction));
    }

    /// <summary>
    /// 技能是否冷却
    /// </summary>
    /// <param name="skill"></param>
    private bool CheckSkillCD(EntitySkill skill)
    {
        if (m_skillCoolingMap.ContainsKey(skill))
        {
            if (Time.time - m_skillCoolingMap[skill] >= skill.CD)
            {
                m_skillCoolingMap[skill] = Time.time;
                return true;
            }
            return false;
        }
        else
        {
            m_skillCoolingMap.Add(skill, Time.time);
            return true;
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
