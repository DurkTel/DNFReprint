using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class Entity 
{
    private enum MoveMode
    {
        NONE = 0,
        INPUT,     //输入移动
        PATH,      //路径移动
        HURT,      //受击移动
    }

    private MoveMode m_moveMode = MoveMode.NONE;

    private Vector2 m_offsetSpeed = new Vector2(1f, 0.7f);

    private float m_curSpeed;

    private float m_moveDirCoefficient = 1f;

    private float m_addMoveForce;

    private bool m_isHitAir;

    private bool m_inputEnabled;

    private Vector2 m_curMoveDir;

    private Vector2 m_velocity;

    /// <summary>
    /// 0 没有移动 1 走路 2跑步
    /// </summary>
    public int movePhase;

    public EntityAttribute entityAttribute;
    /// <summary>
    /// type 0 停止移动 1 开始移动 2 正在移动
    /// </summary>
    public UnityAction<int, int> onMoveEvent;
    

    #region 初始化移动
    private void MoveInit()
    {
        inputReader.moveInputEvent += Move_Input;
        inputReader.buttonMultiEvent += MovePhaseHandle;
    }

    private void Move_Input(Vector2 input)
    {
        if (!m_inputEnabled || (m_moveMode == MoveMode.PATH && input == Vector2.zero) || m_moveMode == MoveMode.HURT)
            return;

        m_moveMode = MoveMode.INPUT;
        m_curMoveDir = input;
    }

    protected void MovePhaseHandle(string action)
    {
        if (!m_inputEnabled || m_moveMode == MoveMode.HURT || status == EntityUnitily.PEACE)
            return;
        if (action == "Left" || action == "Right")
        {
            movePhase = 2;
        }

    }
    public void Move_Stop()
    {
        m_curMoveDir = Vector3.zero;
        if (movePhase != 0)
        {
            movePhase = 0;
            Move_OnEnd();
        }
    }

    private void ReleaseMove()
    {
        m_moveMode = MoveMode.NONE;
        m_curSpeed = 0f;
        m_moveDirCoefficient = 1f;
        m_addMoveForce = 0f;
        m_isHitAir = false;
        m_inputEnabled = false;
        m_curMoveDir = m_velocity = Vector2.zero;
        movePhase = 0;
        entityAttribute = null;
        m_jumpState = JumpState.NONE;
        m_jumpSpeed = 0;
    }
    #endregion

    #region 输入移动
    private void Move_UpdateInput(float fixedDeltaTime)
    {
        if (m_curMoveDir == Vector2.zero && movePhase != 0)
        {
            rigidbody.velocity = Vector2.zero;
            Move_OnEnd();
            return;
        }

        if (m_curMoveDir != Vector2.zero && movePhase == 0)
        {
            Move_OnStart();
        }

        if (IsInThisAni(animationConfig.NotMoveAnim))
            m_curMoveDir = Vector2.zero;
       
        Vector2 dir = m_curMoveDir.normalized * m_offsetSpeed;
        m_curSpeed = movePhase == 1 ? entityAttribute.MoveSpeed : entityAttribute.MoveSpeed * 2;
        m_velocity = dir * m_curSpeed * m_moveDirCoefficient;
        rigidbody.velocity = m_velocity;

        bool onAttack = IsInThisAni(animationConfig.AttackAnim);

        //攻击时不能上下移动
        if (onAttack)
        {
            Vector2 vel = rigidbody.velocity;
            vel.y = 0f;
            rigidbody.velocity = vel;
        }

        //攻击时如果按与面朝方向相反的方向键 不会位移 原地攻击
        if (m_curMoveDir[0] * curFlip < 0 && onAttack)
            rigidbody.velocity = Vector2.zero;

        Move_OnUpdate();
    }

    private void Move_OnStart()
    {
        movePhase = 1;
        onMoveEvent?.Invoke(entityId, 1);
    }

    private void Move_OnEnd()
    {
        movePhase = 0;
        m_moveMode = MoveMode.NONE;
        onMoveEvent?.Invoke(entityId, 0);
    }

    private void Move_OnUpdate()
    {
        if (m_curMoveDir.x != 0 && FilpLimit())
        {
            SetSpriteFilp(m_curMoveDir.x < 0);
        }
        onMoveEvent?.Invoke(entityId, 2);
    }
    #endregion

    #region 路径移动
    private void Move_UpdatePaht(float fixedDeltaTime)
    {

    }
    #endregion

    #region 帧更新
    private void UpdateMove(float fixedDeltaTime)
    {

        switch (m_moveMode)
        {
            case MoveMode.INPUT:
                Move_UpdateInput(fixedDeltaTime);
                break;
            case MoveMode.PATH:
                Move_UpdatePaht(fixedDeltaTime);
                break;
            case MoveMode.HURT:
                Move_UpdateHurt(fixedDeltaTime);
                break;
            default:
                break;
        }

        Move_ForceX(fixedDeltaTime);
        Move_JumpOnUpdate(fixedDeltaTime);
    }
    #endregion

    #region 移动的通用方法
    private void Move_ForceX(float fixedDeltaTime)
    {

        if (Mathf.Abs(m_addMoveForce) > 0.1)
        {
            rigidbody.AddForce(Vector2.right * m_addMoveForce * curFlip, ForceMode2D.Impulse);
            m_addMoveForce = 0f;
        }

    }

    private void SetSpriteFilp(bool isLeft)
    {
        foreach (var item in m_renenderSprites)
        {
            item.SetSpriteFilp(isLeft);
        }
    }


    private bool FilpLimit()
    {
        bool attackAnimLimit = !IsInThisAni(animationConfig.AttackAnim) && !IsInThisAni(animationConfig.NotMoveAnim);
        return attackAnimLimit;
    }
    #endregion
}
