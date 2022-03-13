using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public partial class Entity
{
    /// <summary>
    /// 实体id
    /// </summary>
    public int entityId { get; private set; }
    /// <summary>
    /// 实体类型
    /// </summary>
    public EntityType entityType { get; private set; }
    /// <summary>
    /// 职业类型
    /// </summary>
    public CommonDefine.Career careerType { get; private set; }
    /// <summary>
    /// 皮肤是否已经初始化
    /// </summary>
    public bool skinInitialized { get; private set; }
    /// <summary>
    /// 记录开始初始化的帧数
    /// </summary>
    public int skinInitFrameCount { get; private set; }

    public SkillManager skillManager;

    public GameObject gameObject;

    public Transform transform;

    public void Init(int uid, EntityType type, CommonDefine.Career career , GameObject go)
    {
        entityId = uid;
        entityType = type;
        careerType = career;
        gameObject = go;
        transform = go.transform;

    }

    public void Update()
    {
        TickSpriteAnimation();
        ConditionRelation(m_lastFrame);
    }

    public void LateUpdate()
    { 
        
    }

    public void WaitCreate()
    {
        if (!skinInitialized)
            Skin_CreateAvatar();
    }

    private void AssembleComponent()
    {
        switch (entityType)
        {
            case EntityType.LocalPlayer:
                InitLocalCharacter();
                break;
            case EntityType.OtherPlayer:
                break;
            case EntityType.Monster:
                break;
            case EntityType.Robot:
                break;
            case EntityType.Npc:
                break;
            default:
                break;
        }
    }

    private void InitLocalCharacter()
    {
        gameObject.name = "local_Player";

        //添加动画机
        foreach (AvatarPart part in mainAvatar.avatarPartDic.Values)
        {
            m_renenderSprites.Add(part.renender);
        }
        AnimationConfig = AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/AnimationConfig/Character/Player/SaberAnimConfig.asset", typeof(AnimationConfig)) as AnimationConfig;
        inputReader = InputReader.GetInputAsset();

        //添加输入模块
        m_motor = gameObject.AddComponent<CharactMotor>();
        m_motor.inputReader = InputReader.GetInputAsset();
        m_motor.entity = this;

        //添加技能模块
        skillManager = new SkillManager(this);
        skillManager.inputReader = InputReader.GetInputAsset();
        skillManager.Init();

        //添加属性模块
        EntityAttribute attr = AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/Character/SaberAttr.asset", typeof(EntityAttribute)) as EntityAttribute;
        m_motor.entityAttribute = attr;
    }

    public enum EntityType
    { 
        LocalPlayer = 1,
        OtherPlayer = 2,
        Monster = 3,
        Robot = 4,
        Npc = 5,
    }
}
