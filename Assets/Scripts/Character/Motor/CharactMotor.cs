using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactMotor : BaseMotor
{
    private float m_addMoveForce;


    protected override void Start()
    {
        base.Start();

        InitAnimEvent(EventDefine.EVENT_AIR_ATTACK_COMBO, () =>
        {
            airAttackCombo++;

        });

        InitAnimEvent<float>(EventDefine.EVENT_ADD_JUMP_FORCE, (coefficient) =>
        {
            speedDrop = Mathf.Sqrt(Mathf.Pow(characterAttribute.JumpPower, 2) * coefficient);
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
        bool onAttack = m_spriceAnimator.IsInThisAni(m_animationConfig.attackAnim);
        int flip = m_renenderSprite.GetCurFlip();

        //攻击时不能上下移动
        if (onAttack)
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity[0], 0);

        //攻击时如果按与面朝方向相反的方向键 不会向前位移 原地攻击
        if (m_curMoveDir[0] * flip < 0 && onAttack)
            m_rigidbody.velocity = Vector2.zero;
        else
            m_rigidbody.velocity += new Vector2(1, 0) * m_addMoveForce * flip;

        m_addMoveForce = 0;
    }

    protected override void Update()
    {
        base.Update();
        //SkillUpdate();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

}
