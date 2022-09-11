using System;
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
    public int entityType { get; private set; }
    /// <summary>
    /// 职业类型
    /// </summary>
    public CommonUtility.Career careerType { get; private set; }
    /// <summary>
    /// 实体状态
    /// </summary>
    public int status { get; private set; }
    /// <summary>
    /// 皮肤是否已经初始化
    /// </summary>
    public bool skinInitialized { get; private set; }
    /// <summary>
    /// 皮肤是否已经开始初始化
    /// </summary>
    public bool skinIniting { get; private set; }

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
    public float haltFrame { get; set; }
    /// <summary>
    /// 实体创建完成回调
    /// </summary>
    public static Action<int> onCreateEvent { get; set; }
    /// <summary>
    /// 实体回收完成回调
    /// </summary>
    public static Action<int> onDestroyEvent { get; set; }
    /// <summary>
    /// avatar加载完成
    /// </summary>
    public static Action<int> onLuaAvatarLoadComplete { get; set; }


    public void Init(int uid, int type, CommonUtility.Career career , GameObject go)
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

    public virtual void FixedUpdate(float deltaTime)
    {
        //卡肉顿帧的处理
        UpdateHalt(deltaTime);
        //刷新动画
        UpdateAnimation(deltaTime);
        //刷新移动
        UpdateMove(deltaTime);
    }

    public virtual void Update(float deltaTime)
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

    public void Release()
    {
        entityId = -1;
        m_inputReader = null;
        skinIniting = false;
        //if (updateCollider != null) updateCollider.Clear();
        ReleaseSkin();
        ReleaseCullGroup();
        ReleaseMove();
        ReleaseCollider();
        ReleaseHotRadius();
    }


    public void Dispose()
    {
        onCreateEvent = null;
        onDestroyEvent = null;
    }


    /// <summary>
    /// 改变实体状态
    /// </summary>
    /// <param name="status"></param>
    public void ChangeStatus(int status)
    {
        this.status = status;
        if (animationConfig != null)
        {
            AnimationData animation = this.status == EntityUnitily.PEACE ? animationConfig.idleTown_Anim : animationConfig.idle_Anim;
            DOSpriteAnimation(animation);
        }
    }

    public void SetEntityPosition(Vector3 vector)
    {
        transform.localPosition = vector;
    }

    public void SetInputEnable(bool enable)
    {
        if (enable)
        {
            inputReader.EnableGameplayInput();
            m_inputEnabled = true;
        }
        else
        {
            inputReader.DisableAllInPut();
            m_inputEnabled = false;
        }
    }

    public void AddEntityAttribute(string name)
    {
        //以后改成lua赋值
        //EntityAttribute attr = AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/Character/SaberAttr.asset", typeof(EntityAttribute)) as EntityAttribute;
        //EntityAttribute attr = AssetDatabase.LoadAssetAtPath(name, typeof(EntityAttribute)) as EntityAttribute;
        //entityAttribute = attr;

        entityAttribute = AssetLoader.Load<EntityAttribute>("so/SaberAttr");
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
        if (haltFrame > 0) 
        { 
            haltFrame -= deltaTime * CommonUtility.HardStraight * 100f;
            haltFrame = Mathf.Clamp(haltFrame, 0, CommonUtility.HaltFrameMax);
        }
    }

}
