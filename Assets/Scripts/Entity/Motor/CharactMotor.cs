using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactMotor : EntityMotor
{
    [HideInInspector]
    public int airAttackCombo;



    //private void OnEnable()
    //{
    //    inputReader.moveInputEvent += ReceiveMoveInput;
    //    inputReader.buttonMultiEvent += MovePhaseHandle;
    //}

    private void OnDisable()
    {
        inputReader.moveInputEvent -= ReceiveMoveInput;
        inputReader.buttonMultiEvent -= MovePhaseHandle;

    }


    protected override void Start()
    {
        base.Start();
        inputReader.moveInputEvent += ReceiveMoveInput;
        inputReader.buttonMultiEvent += MovePhaseHandle;
        inputReader.EnableGameplayInput();

        
    }

    protected override void InitEvent()
    {
        base.InitEvent();

        InitAnimEvent(EventDefine.EVENT_AIR_ATTACK_COMBO, () =>
        {
            airAttackCombo++;

        });

        InitAnimEvent<float>(EventDefine.EVENT_ADD_JUMP_FORCE, (coefficient) =>
        {
            speedDrop = Mathf.Sqrt(Mathf.Pow(entityAttribute.JumpPower, 2) * coefficient);
            airAttackCombo = 0;
        });

        InitAnimEvent(EventDefine.EVENT_RESET_MOVEPHASE, () =>
        {
            movePhase = m_curMoveDir != Vector2.zero ? 1 : 0;
        });

    }

    protected override void MotorMove()
    {
        base.MotorMove();
        bool onAttack = entity.IsInThisAni(m_animationConfig.AttackAnim);
        int flip = m_renenderSprites[0].GetCurFlip();

        //攻击时不能上下移动
        if (onAttack)
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity[0], 0);

        //攻击时如果按与面朝方向相反的方向键 不会向前位移 原地攻击
        if (m_curMoveDir[0] * flip < 0 && onAttack)
            m_rigidbody.velocity = Vector2.zero;

    }

    private void ReceiveMoveInput(Vector2 input)
    {

        m_curMoveDir = input;
        if (entity.IsInThisAni(m_animationConfig.NotMoveAnim))
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

    //public override void GetDamage(EntitySkill entitySkill = null)
    //{
    //    base.GetDamage(entitySkill);

    //}



    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }


}
