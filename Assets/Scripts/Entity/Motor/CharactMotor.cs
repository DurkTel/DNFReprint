using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactMotor : EntityMotor
{
    [HideInInspector]
    public int airAttackCombo;

    public InputReader inputReader;

    private float m_addMoveForce;

    private void OnEnable()
    {
        inputReader.moveInputEvent += ReceiveMoveInput;
        inputReader.buttonMultiEvent += MovePhaseHandle;
    }

    private void OnDisable()
    {
        inputReader.moveInputEvent -= ReceiveMoveInput;
        inputReader.buttonMultiEvent -= MovePhaseHandle;

    }


    protected override void Start()
    {
        base.Start();

        InitEvent();
        inputReader.EnableGameplayInput();

        
    }

    private void InitEvent()
    {
        InitAnimEvent(EventDefine.EVENT_AIR_ATTACK_COMBO, () =>
        {
            airAttackCombo++;

        });

        InitAnimEvent<float>(EventDefine.EVENT_ADD_JUMP_FORCE, (coefficient) =>
        {
            speedDrop = Mathf.Sqrt(Mathf.Pow(entityAttribute.JumpPower, 2) * coefficient);
            airAttackCombo = 0;
        });

        InitAnimEvent<float>(EventDefine.EVENT_MOVE_COEFFICIENT_CHANGE, (coefficient) =>
        {
            m_moveDirCoefficient = coefficient;
        });

        InitAnimEvent<float>(EventDefine.EVENT_ADD_MOVE_FORCE, (coefficient) =>
        {
            m_addMoveForce = coefficient;
        });

        InitAnimEvent<float>(EventDefine.EVENT_REST_MOVE_SPEED, (coefficient) =>
        {
            m_curSpeed = coefficient;
        });
    }

    protected override void MotorMove()
    {
        base.MotorMove();
        bool onAttack = m_spriceAnimator.IsInThisAni(m_animationConfig.AttackAnim);
        int flip = m_renenderSprite.GetCurFlip();

        //攻击时不能上下移动
        if (onAttack)
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity[0], 0);

        //攻击时如果按与面朝方向相反的方向键 不会向前位移 原地攻击
        if (m_curMoveDir[0] * flip < 0 && onAttack)
            m_rigidbody.velocity = Vector2.zero;
        else
        {
            if (Mathf.Abs(m_addMoveForce) > 0.1)
            {
                if (m_addMoveForce > 0)
                    m_addMoveForce -= Time.deltaTime * 3;
                else
                    m_addMoveForce += Time.deltaTime * 3;
                m_rigidbody.velocity += new Vector2(1, 0) * m_addMoveForce * flip;

            }

        }

    }

    private void ReceiveMoveInput(Vector2 input)
    {

        m_curMoveDir = input;
        if (m_spriceAnimator.IsInThisAni(m_animationConfig.NotMoveAnim))
            movePhase = 0;
        else if (m_curMoveDir != Vector2.zero && movePhase != 2)
            movePhase = 1;
        else if (m_curMoveDir == Vector2.zero)
            movePhase = 0;
    }

    private void MovePhaseHandle(string action)
    {
        if (action == "Left" || action == "Right")
        {
            movePhase = 2;
        }

    }

    public override void GetDamage(EntitySkill entitySkill = null)
    {
        movePhase = 0;
        m_hitRecoverTime = 500 / (entityAttribute.HitRecover + 1);
        if (m_charactRenderer.localPosition.y > 0)
        {
            //空中受击直接浮空 加速下落
            GetAirBorne(entitySkill, Mathf.Abs(speedDrop * 2f));
        }
        else
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
                m_spriceAnimator.DOSpriteAnimation(m_animationConfig.hit1_Anim);
            else
                m_spriceAnimator.DOSpriteAnimation(m_animationConfig.hit2_Anim);

            GetAirBorne(entitySkill);

        }

    }

    public override void GetAirBorne(EntitySkill entitySkill, float air = 0)
    {
        if (entitySkill.CanAirBorne || air != 0)
        {
            float airForce = air == 0 ? entitySkill.AirBorneForce - entityAttribute.AirBorneLimit : air;
            m_addMoveForce = -airForce / 2.5f;
            m_spriceAnimator.DOSpriteAnimation(m_animationConfig.airBorne_Anim);
            speedDrop = airForce;
        }
    }


    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.I))
        {
            GetDamage();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            //GetAirBorne();
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }


}
