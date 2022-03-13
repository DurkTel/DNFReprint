using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

/// <summary>
/// 该类管理实体的移动、跳跃等
/// </summary>
public class EntityMotor : BaseEvent, IDamage
{
    private Vector2 m_offsetSpeed = new Vector2(1f, 0.7f);

    protected Rigidbody2D m_rigidbody;

    protected RenenderSprite[] m_renenderSprites;

    protected AnimationConfig m_animationConfig;

    protected Transform m_charactRenderer;

    protected float m_moveDirCoefficient = 1f;

    protected Vector2 m_curMoveDir;

    protected float m_curSpeed;

    protected float m_hitRecoverTime;

    protected float m_addMoveForce;

    protected bool m_isHitAir;

    public Entity entity;

    public InputReader inputReader;

    public bool isHitRecover { get { return m_hitRecoverTime > 0; } }
    [HideInInspector]
    public bool runningReady;
    [HideInInspector]
    public bool walkingReady;
    /// <summary>
    /// 0 没有移动 1 走路 2跑步
    /// </summary>
    public int movePhase;

    public EntityAttribute entityAttribute;

    public bool isStatic;


    public Vector2 curMoveDir { get { return m_curMoveDir; } }

    [HideInInspector] public float speedDrop;

    protected virtual void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_renenderSprites = GetComponentsInChildren<RenenderSprite>();
        //m_entity = GetComponentInChildren<SpriteAnimator>();
        m_charactRenderer = entity.skinNode;
        m_animationConfig = entity.AnimationConfig;
        entity.DOSpriteAnimation(m_animationConfig.idle_Anim);

        InitEvent();

    }

    protected virtual void Update()
    {
        MotorAnim();
    }

    protected virtual void FixedUpdate()
    {
        MotorMove();
        DropUpdate();
    }

    protected virtual void InitEvent()
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
            speedDrop = Mathf.Sqrt(Mathf.Pow(1.2f, 2) * coefficient);
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

    protected virtual void MotorAnim()
    {
        if (m_curMoveDir.x != 0 && FilpLimit())
        {
            SetSpriteFilp(m_curMoveDir.x < 0);
        }

        CalculateHitRecover();
    }

    protected void SetSpriteFilp(bool isLeft)
    {
        foreach (var item in m_renenderSprites)
        {
            item.SetSpriteFilp(isLeft);
        }
    }

    protected virtual void MotorMove()
    {
        if (entity.IsInThisAni(m_animationConfig.NotMoveAnim))
            m_curMoveDir = Vector2.zero;
        //移动向量由子类自身处理
        Vector2 dir = m_curMoveDir.normalized * m_offsetSpeed;
        m_curSpeed = movePhase == 1 ? entityAttribute.MoveSpeed : entityAttribute.MoveSpeed * 2;
        m_rigidbody.velocity = dir * m_curSpeed * m_moveDirCoefficient;

        int flip = m_renenderSprites[0].GetCurFlip();

        if (Mathf.Abs(m_addMoveForce) > 0.1)
        {
            if (m_addMoveForce > 0)
                m_addMoveForce -= Time.deltaTime * 3;
            else
                m_addMoveForce += Time.deltaTime * 3;
            m_rigidbody.velocity += new Vector2(1, 0) * m_addMoveForce * flip;

        }

    }

    protected void CalculateHitRecover()
    {
        if (m_hitRecoverTime > 0)
            m_hitRecoverTime -= Time.deltaTime;
    }

    private bool FilpLimit()
    {
        bool attackAnimLimit = !entity.IsInThisAni(m_animationConfig.AttackAnim) && !entity.IsInThisAni(m_animationConfig.NotMoveAnim);
        return attackAnimLimit;
    }

    /// <summary>
    /// 浮空相关的处理
    /// </summary>
    private void DropUpdate()
    {
        if (isStatic) return;
        //空中处于受击动画停顿 减缓下落
        bool hurtPause = entity.IsInThisAni(m_animationConfig.HitAnim) && m_isHitAir;
        if (hurtPause)
        {
            speedDrop += Time.deltaTime * 15f * 0.6f;
            return;
        } 
        m_charactRenderer.localPosition += Vector3.up * speedDrop * Time.fixedDeltaTime;
        if (m_charactRenderer.localPosition.y > 0)
        {
            if (speedDrop > 0)
            {
                speedDrop -= Time.fixedDeltaTime * 15f * 0.8f;
            }
            else
            {
                speedDrop -= Time.fixedDeltaTime * 15f * 0.6f;
            }
        }
        else
            m_isHitAir = false;//退出受击浮空状态
        //if (m_charactRenderer.localPosition.y <= 0)
        //    speedDrop = 0;


        m_charactRenderer.localPosition = new Vector3(m_charactRenderer.localPosition.x, Mathf.Clamp(m_charactRenderer.localPosition.y, 0, Mathf.Infinity), m_charactRenderer.localPosition.z);
    }

    public virtual void GetDamage(OtherInfo info)
    {
        if (!isStatic)
        {
            float deltaX = info.collider2d.transform.position.x - transform.position.x;
            SetSpriteFilp(deltaX < 0);
        }
        movePhase = 0;
        m_hitRecoverTime = 500 / (entityAttribute.HitRecover + 1);
        EntitySkill entitySkill = SkillConfig.GetInfoByCode(info.otherCollInfo.skillCode);
        if (m_charactRenderer.localPosition.y > 0)
        {
            if (!m_isHitAir)
            {
                //空中正常状态受击直接浮空 加速下落
                //GetAirBorne(info.otherCollInfo.entitySkill, Mathf.Abs(speedDrop * 2f));
            }
            else
            {
                //已经浮空状态下 再收到攻击
                entity.DOSpriteAnimation(m_animationConfig.HitAnim[0]);
                if (entitySkill != null && entitySkill.CanAirBorne)
                    GetAirBorne(entitySkill);

            }
        }
        else
        {
            int rand = Random.Range(0, m_animationConfig.HitAnim.Count);
            entity.DOSpriteAnimation(m_animationConfig.HitAnim[rand]);

            GetAirBorne(entitySkill);

        }

        MusicManager.Instance.PlaySound("sm_dmg_01");
        int randsound = Random.Range(1, 3);
        MusicManager.Instance.PlaySound("weapon/beamswda_hit_0" + randsound);

    }

    public virtual void GetAirBorne(EntitySkill entitySkill, float air = 0)
    {
        if ((entitySkill.CanAirBorne || air != 0) && !isStatic)
        {
            float airForce = air == 0 ? entitySkill.AirBorneForce - entityAttribute.AirBorneLimit : air;
            m_addMoveForce = -airForce / 5f;
            entity.DOSpriteAnimation(m_animationConfig.airBorne_Anim);
            speedDrop = airForce;
            m_isHitAir = true;
        }
    }
}
