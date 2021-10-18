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

    private Vector2 m_curMoveDir;

    private float m_curSpeed;

    public int airAttackCombo;

    public CharacterAttribute characterAttribute;

    public CharacterSkillTree characterSkillTree = new CharacterSkillTree();

    public CharacterSkill jumpAttack;//先写死测试

    [HideInInspector] public float speedDrop;


    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_renenderSprite = GetComponentInChildren<RenenderSprite>();
        m_spriceAnimator = GetComponentInChildren<SpriteAnimator>();
        m_charactRenderer = transform.Find("SpriteRenderer").GetComponent<Transform>();
        m_animationConfig = m_spriceAnimator.AnimationConfig;
        characterSkillTree.AddSkill(jumpAttack);
        InitAnimEvent();
    }

    protected virtual void Update()
    {
        MotorAnim();
        if (Input.GetKeyDown(KeyCode.P))
        {
            m_spriceAnimator.DOSpriteAnimation(m_animationConfig.attack_Anim);
        }
    }

    protected virtual void FixedUpdate()
    {
        MotorMove();
        DropUpdate();
    }

    protected virtual void InitAnimEvent()
    {
        m_spriceAnimator.EVENT_AddJump += () => 
        {
            speedDrop = Mathf.Sqrt(Mathf.Pow(characterAttribute.JumpPower, 2) * 15);
            airAttackCombo = 0;
        };

        
    }

    protected virtual bool IsMotorMoveLimit()
    {
        return (m_spriceAnimator.current_animationData != m_animationConfig.jump_Anim) &&
            (m_spriceAnimator.current_animationData != m_animationConfig.jumpEnd_Anim) &&
            (m_spriceAnimator.current_animationData != m_animationConfig.jump_Attack) &&
            (m_spriceAnimator.current_animationData != m_animationConfig.jumpRise_Anim) &&
            (m_spriceAnimator.current_animationData != m_animationConfig.jumpDrop_Anim) &&
            (m_spriceAnimator.current_animationData != m_animationConfig.attack_Anim) &&
            (m_spriceAnimator.current_animationData != m_animationConfig.attack2_Anim) &&
            (m_spriceAnimator.current_animationData != m_animationConfig.attack3_Anim);
    }

    protected virtual bool IsMotorJumpLimit()
    {
        return (m_spriceAnimator.current_animationData != m_animationConfig.jump_Anim) &&
            (m_spriceAnimator.current_animationData != m_animationConfig.jumpDrop_Anim) &&
            (m_spriceAnimator.current_animationData != m_animationConfig.jumpEnd_Anim);
    }

    protected virtual void MotorAnim()
    {
        m_curSpeed = ControlSpeed();

        if (IsMotorJumpLimit() && KeyboardInput.Instance.ButtonJump.OnPressed)
        {
            m_spriceAnimator.DOSpriteAnimation(m_animationConfig.jump_Anim);
        }
        else if (IsMotorMoveLimit())
        {
            if (m_curMoveDir != Vector2.zero)
            {
                m_spriceAnimator.DOSpriteAnimation(m_curSpeed > characterAttribute.MoveSpeed ? m_animationConfig.run_Anim : m_animationConfig.walk_Anim);
            }
            else
            {
                m_spriceAnimator.DOSpriteAnimation(m_animationConfig.idle_Anim);
            }
        }

        if (m_curMoveDir.x != 0)
            m_renenderSprite.SetSpriteFilp(m_curMoveDir.x < 0);
    }

    protected virtual void MotorMove()
    {
        m_curMoveDir.x = KeyboardInput.Instance.MoveAxisX.Axis * XOffsetSpeed;
        m_curMoveDir.y = KeyboardInput.Instance.MoveAxisY.Axis * YOffsetSpeed;

        m_curMoveDir = m_curMoveDir * m_curSpeed;
        m_rigidbody.velocity = m_curMoveDir;
       
    }

    protected virtual float ControlSpeed()
    {
        if ((KeyboardInput.Instance.MoveLeft.OnPressed && KeyboardInput.Instance.MoveLeft.IsExtending) ||
            (KeyboardInput.Instance.MoveRight.OnPressed && KeyboardInput.Instance.MoveRight.IsExtending))
        {
            return characterAttribute.MoveSpeed * 2;
        }
        else if ((!KeyboardInput.Instance.MoveAxisX.IsPressing && !KeyboardInput.Instance.MoveAxisY.IsDelaying)||
            !IsMotorMoveLimit())
            return characterAttribute.MoveSpeed;
        return m_curSpeed != 0 ? m_curSpeed : characterAttribute.MoveSpeed;
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
