using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BaseMotor : MonoBehaviour
{
    const float XOffsetSpeed = 1f;

    const float YOffsetSpeed = 0.7f;

    protected Rigidbody2D m_rigidbody;

    protected RenenderSprite m_renenderSprite;

    protected AnimationConfig m_animationConfig;

    protected SpriteAnimator m_spriceAnimator;

    protected Transform m_charactRenderer;

    protected float m_moveDirCoefficient = 1f;

    protected Vector2 m_curMoveDir;

    protected float m_curSpeed;

    public int airAttackCombo;

    public bool runningReady;

    public bool walkingReady;

    public CharacterAttribute characterAttribute;

    public CharacterSkillTree characterSkillTree;

    public CharacterSkill jumpAttack;//先写死测试

    public Vector2 curMoveDir { get { return m_curMoveDir; } }

    [HideInInspector] public float speedDrop;

    private void Awake()
    {
        
    }

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_renenderSprite = GetComponentInChildren<RenenderSprite>();
        m_spriceAnimator = GetComponentInChildren<SpriteAnimator>();
        characterSkillTree = GetComponent<CharacterSkillTree>();
        m_charactRenderer = transform.Find("SpriteRenderer").GetComponent<Transform>();
        m_animationConfig = m_spriceAnimator.AnimationConfig;
        characterSkillTree.AddSkill(jumpAttack);
        InitAnimEvent();
        m_spriceAnimator.DOSpriteAnimation(m_animationConfig.commonAnim.idle_Anim);
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

    protected virtual void InitAnimEvent()
    {

    }

    protected virtual void MotorAnim()
    {
        m_curSpeed = ControlSpeed();

        runningReady = m_curSpeed > characterAttribute.MoveSpeed && m_curMoveDir != Vector2.zero;
        walkingReady = m_curSpeed <= characterAttribute.MoveSpeed && m_curMoveDir != Vector2.zero;

        if (m_curMoveDir.x != 0 && FilpLimit())
            m_renenderSprite.SetSpriteFilp(m_curMoveDir.x < 0);
    }

    protected virtual void MotorMove()
    {
        m_curMoveDir.x = KeyboardInput.Instance.MoveAxisX.Axis * XOffsetSpeed;
        m_curMoveDir.y = KeyboardInput.Instance.MoveAxisY.Axis * YOffsetSpeed;

        m_curMoveDir = m_curMoveDir * m_curSpeed * m_moveDirCoefficient;
        m_rigidbody.velocity = m_curMoveDir;
       
    }

    protected virtual float ControlSpeed()
    {
        if ((KeyboardInput.Instance.MoveLeft.OnPressed && KeyboardInput.Instance.MoveLeft.IsExtending) ||
            (KeyboardInput.Instance.MoveRight.OnPressed && KeyboardInput.Instance.MoveRight.IsExtending))
        {
            return characterAttribute.MoveSpeed * 2;
        }
        else if ((!KeyboardInput.Instance.MoveAxisX.IsPressing && !KeyboardInput.Instance.MoveAxisY.IsDelaying))
            return characterAttribute.MoveSpeed;
        return m_curSpeed != 0 ? m_curSpeed : characterAttribute.MoveSpeed;
    }

    private bool FilpLimit()
    {
        bool attackAnimLimit = !m_spriceAnimator.IsInThisAni(m_animationConfig.attackAnim);
        return attackAnimLimit;
    }

    /// <summary>
    /// 浮空相关的处理
    /// </summary>
    private void DropUpdate()
    {
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
