using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class Entity 
{
    private enum JumpState : int
    {
        NONE = 0,
        START,      //开始跳跃
        RISE,       //上升阶段
        PEAK,       //最高点阶段
        DROP,       //下落阶段
        FALL        //空中受击掉落
    }

    private JumpState m_jumpState = JumpState.NONE;

    private float m_jumpSpeed;

    private float m_gravity = CommonDefine.GravitationalAcceleration;

    /// <summary>
    /// 跳跃事件 type 1开始跳跃 2上升阶段 3达到最高点 4下落阶段 5受击掉落 6着地
    /// </summary>
    public UnityAction<int, int> onJumpEvent;


    private void JumpInit()
    {
        inputReader.buttonPressEvent += InputJump;
    }

    private void InputJump(string name)
    {
        if (!m_inputEnabled)
            return;
        if (name == "Jump")
            Move_Jump();
    }

    public void Move_Jump()
    {
        //处于跳跃中 直接跳出
        if (m_jumpState != JumpState.NONE)
            return;

        Move_JumpOnStart();

    }
    private void Move_JumpOnStart()
    {
        m_jumpState = JumpState.START;
        onJumpEvent?.Invoke(entityId, 1);

        //达到目标高度所需的初速度
        m_jumpSpeed = Mathf.Sqrt(2f * m_gravity * entityAttribute.JumpHeight);

        //播放起跳动画
        DOSpriteAnimation(animationConfig.jump_Anim);
        animationFinish = () =>
        {
            m_jumpState = JumpState.RISE;
        };
    }

    private void Move_JumpOnEnd()
    {
        m_jumpSpeed = 0f;
        m_jumpState = JumpState.NONE;
        skinNode.localPosition = Vector3.zero;
        onJumpEvent?.Invoke(entityId, 6);
    }

    private void Move_JumpOnUpdate(float fixedDeltaTime)
    {
        if (m_jumpSpeed == 0f) return;
        m_jumpState = m_jumpSpeed > 0 ? JumpState.RISE : JumpState.DROP;

        float deltaY = m_jumpSpeed * fixedDeltaTime - 0.5f * m_gravity * Mathf.Pow(fixedDeltaTime, 2);
        m_jumpSpeed = m_jumpSpeed - m_gravity * fixedDeltaTime;

        Vector3 deltaPosition = skinNode.localPosition;
        deltaPosition.y = deltaY;
        skinNode.localPosition += deltaPosition;
        
        if (Mathf.Abs(m_jumpSpeed) <= 0.05f)
        {
            m_jumpState = JumpState.PEAK;
            onJumpEvent?.Invoke(entityId, 3);
        }
        else if (skinNode.localPosition.y <= 0f)
        {
            Move_JumpOnEnd();
        }

        onJumpEvent?.Invoke(entityId, (int)m_jumpState);
    }
}
