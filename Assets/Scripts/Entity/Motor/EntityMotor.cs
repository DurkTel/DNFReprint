using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class EntityMotor : BaseEvent
{
    const float XOffsetSpeed = 1f;

    const float YOffsetSpeed = 0.7f;

    private Vector2 m_offsetSpeed = new Vector2(1f, 0.7f);

    protected Rigidbody2D m_rigidbody;

    protected RenenderSprite m_renenderSprite;

    protected AnimationConfig m_animationConfig;

    protected SpriteAnimator m_spriceAnimator;

    protected Transform m_charactRenderer;

    protected float m_moveDirCoefficient = 1f;

    protected Vector2 m_curMoveDir;

    protected float m_curSpeed;
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

    private void Awake()
    {
        
    }

    protected virtual void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_renenderSprite = GetComponentInChildren<RenenderSprite>();
        m_spriceAnimator = GetComponentInChildren<SpriteAnimator>();
        m_charactRenderer = transform.Find("SpriteRenderer").GetComponent<Transform>();
        m_animationConfig = m_spriceAnimator.AnimationConfig;
        m_spriceAnimator.DOSpriteAnimation(m_animationConfig.CommonAnim.idle_Anim);
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

    protected virtual void MotorAnim()
    {
        if (m_curMoveDir.x != 0 && FilpLimit())
            m_renenderSprite.SetSpriteFilp(m_curMoveDir.x < 0);
    }

    protected virtual void MotorMove()
    {
        //移动向量由子类自身处理
        Vector2 dir = m_curMoveDir.normalized * m_offsetSpeed;
        m_curSpeed = movePhase == 1 ? entityAttribute.MoveSpeed : entityAttribute.MoveSpeed * 2;
        m_rigidbody.velocity = dir * m_curSpeed * m_moveDirCoefficient;

    }


    private bool FilpLimit()
    {
        bool attackAnimLimit = !m_spriceAnimator.IsInThisAni(m_animationConfig.AttackAnim);
        return attackAnimLimit;
    }

    /// <summary>
    /// 浮空相关的处理
    /// </summary>
    private void DropUpdate()
    {
        if (isStatic) return;
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

        if (m_charactRenderer.localPosition.y <= 0)
            speedDrop = 0;


        m_charactRenderer.localPosition = new Vector3(m_charactRenderer.localPosition.x, Mathf.Clamp(m_charactRenderer.localPosition.y, 0, Mathf.Infinity), m_charactRenderer.localPosition.z);
    }

}
