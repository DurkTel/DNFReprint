﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public partial class Entity : BaseEvent
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
    public CommonUtility.Career careerType { get; private set; }
    /// <summary>
    /// 皮肤是否已经初始化
    /// </summary>
    public bool skinInitialized { get; private set; }
    /// <summary>
    /// 记录开始初始化的帧数
    /// </summary>
    public int skinInitFrameCount { get; private set; }

    private InputReader m_inputReader;
    /// <summary>
    /// 输入模块
    /// </summary>
    public InputReader inputReader
    {
        get
        {
            if(m_inputReader == null)
                m_inputReader = InputReader.GetInputAsset();

            return m_inputReader;
        }
    }
    /// <summary>
    /// 技能管理
    /// </summary>
    public SkillManager skillManager;
    /// <summary>
    /// 当前游戏对象
    /// </summary>
    public GameObject gameObject;
    /// <summary>
    /// 当前变换组件
    /// </summary>
    public Transform transform;
    /// <summary>
    /// 顿帧时间（卡肉感）
    /// </summary>
    private float m_haltFrame;

    public void Init(int uid, EntityType type, CommonUtility.Career career , GameObject go)
    {
        entityId = uid;
        entityType = type;
        careerType = career;
        gameObject = go;
        transform = go.transform;

        InitEvent();
        MoveInit();
        JumpInit();
        CullGroupInit();
        ColliderInit();
    }

    public void FixedUpdate(float deltaTime)
    {
        //卡肉顿帧的处理
        UpdateHalt(deltaTime);
        //刷新动画
        UpdateAnimation(deltaTime);
        //刷新移动
        UpdateMove(deltaTime);
    }

    public void Update(float deltaTime)
    {
        
        //刷新剔除
        CullGroupUpdate(deltaTime);
    }

    public void LateUpdate()
    { 
        
    }

    public void WaitCreate()
    {
        if (!skinInitialized)
            Skin_CreateAvatar();
    }

    public void GetHurt(DamageData damage)
    {
        m_hurtSource = damage;
        m_moveMode = MoveMode.HURT;
    }

    public void Release()
    {
        entityId = -1;
        m_inputReader = null;
        skinInitFrameCount = -1;
        //if (updateCollider != null) updateCollider.Clear();
        ReleaseSkin();
        ReleaseCullGroup();
        ReleaseMove();
        ReleaseCollider();
    }

    private void AssembleComponent()
    {
        switch (entityType)
        {
            case EntityType.LocalPlayer:
                InitLocalCharacter();
                break;
            case EntityType.OtherPlayer:
                InitOtherCharacter();
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

        skinNode.gameObject.AddComponent<SortSprite2D>();
    }

    private void InitLocalCharacter()
    {
        gameObject.name = "local_Player";

        //添加动画机
        foreach (AvatarPart part in mainAvatar.avatarPartDic.Values)
        {
            m_renenderSprites.Add(part.renender);
        }
        animationConfig = AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/AnimationConfig/Character/Player/SaberAnimConfig.asset", typeof(AnimationConfig)) as AnimationConfig;

        //添加技能模块
        skillManager = new SkillManager(this);
        skillManager.inputReader = InputReader.GetInputAsset();
        skillManager.Init();

        //添加属性模块
        EntityAttribute attr = AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/Character/SaberAttr.asset", typeof(EntityAttribute)) as EntityAttribute;
        entityAttribute = attr;

        //添加碰撞检测
        //GameObject coller = new GameObject("ColliderRoot");
        //coller.transform.SetParent(transform);
        //updateCollider = coller.AddComponent<UpdateCollider>();
        //updateCollider.Init(this);

        inputReader.EnableGameplayInput();
        m_inputEnabled = true;
        OrbitCamera.Instance.focus = allBones["CameraTarget"];
        DOSpriteAnimation(animationConfig.idle_Anim);
    }

    private void InitOtherCharacter()
    {
        gameObject.name = "other_Player";

        //添加动画机
        foreach (AvatarPart part in mainAvatar.avatarPartDic.Values)
        {
            m_renenderSprites.Add(part.renender);
        }
        animationConfig = AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/AnimationConfig/Character/Player/SaberAnimConfig.asset", typeof(AnimationConfig)) as AnimationConfig;

        //添加技能模块
        skillManager = new SkillManager(this);

        //添加属性模块
        EntityAttribute attr = AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/Character/SaberAttr.asset", typeof(EntityAttribute)) as EntityAttribute;
        entityAttribute = attr;

        //添加碰撞检测
        //GameObject coller = new GameObject("ColliderRoot");
        //coller.transform.SetParent(transform);
        //updateCollider = coller.AddComponent<UpdateCollider>();
        //updateCollider.Init(this);

        DOSpriteAnimation(animationConfig.idle_Anim);
    }

    private void InitEvent()
    {

        InitAnimEvent<float>(EventDefine.EVENT_MOVE_COEFFICIENT_CHANGE, (coefficient) =>
        {
            m_moveDirCoefficient = coefficient;
        });

        InitAnimEvent<float>(EventDefine.EVENT_ADD_MOVEX_FORCE, (coefficient) =>
        {
            if (m_isHitAir) return;
            m_addMoveForce = coefficient;
        });

        InitAnimEvent<float>(EventDefine.EVENT_ADD_MOVEY_FORCE, (coefficient) =>
        {
            m_jumpSpeed = Mathf.Sqrt(2f * m_gravity * coefficient);
        });

        InitAnimEvent<float>(EventDefine.EVENT_REST_MOVE_SPEED, (coefficient) =>
        {
            m_curSpeed = coefficient;
        });

        InitAnimEvent<string>(EventDefine.EVENT_PLAY_SOUND, (name) =>
        {
            MusicManager.Instance.PlaySound(name);
        });
    }


    private void UpdateHalt(float deltaTime)
    {
        if (m_haltFrame > 0) { m_haltFrame -= deltaTime * CommonUtility.HardStraight * 1000; }
        m_haltFrame = Mathf.Clamp(m_haltFrame, 0, CommonUtility.HaltFrameMax);
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
