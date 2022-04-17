using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Entity
{

    private float m_hitRecoverTime;
    public bool isHitRecover { get { return m_hitRecoverTime <= 0f; } }

    private bool m_fallDown;

    private bool m_airborne;

    private float m_hitVelocityX;

    private float m_hitVelocityY;

    private float m_inithitVelocityY;

    private int m_rebound;

    private float m_standTime;

    #region 受击移动
    private void Move_UpdateHurt(float fixedDeltaTime)
    {
        m_hitRecoverTime -= fixedDeltaTime;

        if (m_hurtSource != null && m_moveMode == MoveMode.HURT)
        {
            m_hitRecoverTime -= fixedDeltaTime;
            Move_UpdateHurt_X(fixedDeltaTime);
            Move_UpdateHurt_Y(fixedDeltaTime);
            m_hitVelocityX = m_hitVelocityX * -curFlip;
            rigidbody.velocity = new Vector2(m_hitVelocityX, 0);

            //没浮空 硬直消失后回复状态
            if (m_hitRecoverTime <= 0f && m_inithitVelocityY <= 0f)
                MoveHurt_OnEnd();
        }

        MoveHurt_UpdateStand(fixedDeltaTime);

    }

    private void Move_UpdateHurt_X(float fixedDeltaTime)
    {
        if (Mathf.Abs(m_hitVelocityX) > 0)
        {
            m_hitVelocityX *= CommonUtility.AirFriction * m_hurtSource.acceleration * fixedDeltaTime;
        }
    }

    private void Move_UpdateHurt_Y(float fixedDeltaTime)
    {
        m_airborne = skinNode.localPosition.y > 0;
        if (Mathf.Abs(m_inithitVelocityY) > 0 && isHitRecover)
        {
            m_hitVelocityY -= CommonUtility.AirFriction * m_gravity * fixedDeltaTime;
            float deltaY = m_hitVelocityY * fixedDeltaTime - 0.5f * m_gravity * Mathf.Pow(fixedDeltaTime, 2);
            m_hitVelocityY -= m_gravity * fixedDeltaTime;
            if (m_hitVelocityY < 0 && skinNode.localPosition.y > 0)
            {
                DOSpriteAnimation(animationConfig.airBorne_Anim);
            }

            Vector3 deltaPosition = skinNode.localPosition;
            deltaPosition.y = deltaY;
            skinNode.localPosition += deltaPosition;
            if (skinNode.localPosition.y <= 0f)
            {
                if (Mathf.Abs(m_hitVelocityY) >= 5f && m_rebound < 1)//落地速度足够 反弹
                {
                    m_rebound++;
                    m_hitVelocityY = Mathf.Abs(m_hitVelocityY) * 0.5f;
                }
                else if (m_hitVelocityY < 0)
                {
                    m_airborne = false;
                    m_hurtSource = null;
                    DOSpriteAnimation(animationConfig.lie_Anim);
                    m_fallDown = true;
                    m_standTime = Time.realtimeSinceStartup + 2f;
                }

            }
        }
    }

    /// <summary>
    /// 倒地起立
    /// </summary>
    /// <param name="fixedDeltaTime"></param>
    private void MoveHurt_UpdateStand(float fixedDeltaTime)
    {
        if (!m_fallDown) return;
        m_standTime -= fixedDeltaTime;
        if (Time.realtimeSinceStartup >= m_standTime)
        {
            m_fallDown = false;
            DOSpriteAnimation(animationConfig.sit_Anim);
            animationFinish = MoveHurt_OnEnd;
        }
    }

    private void MoveHurt_OnStart(DamageData damage)
    {
        m_hurtSource = damage;
        Move_Stop();
        m_rebound = 0;
        m_standTime = 0f;
        m_hitRecoverTime = m_hurtSource.RecoverTime;
        //伤害来源是实体
        if (damage.attacker != null)
        {
            //面对伤害源
            if (damage.lookAttacker)
            {
                float deltaX = damage.attacker.transform.position.x - transform.position.x;
                SetSpriteFilp(deltaX < 0);
            }

            //浮空或倒地时再次 浮空要特殊处理
            float height = m_hurtSource.heightY;
            if (m_airborne && skinNode.localPosition.y <= 0.65f)
                height *= 0.6f;
            else if (m_fallDown)
            { 
                height *= 0.5f;
                m_standTime += m_hitRecoverTime;
            }

            
            m_inithitVelocityY = m_hitVelocityY = m_airborne && height == 0f ? m_hitVelocityY : Mathf.Sqrt(CommonUtility.GravitationalAcceleration * Mathf.Pow(height, 2));
            
            m_hitVelocityX = Mathf.Abs(m_inithitVelocityY) > 0f ? damage.velocityXY : m_hurtSource.velocityX; //浮空时X轴速度使用XY的配置

            int index = m_inithitVelocityY > 0 ? 0 : 1;
            DOSpriteAnimation(animationConfig.HitAnim[index]);
        }

        m_moveMode = MoveMode.HURT;
    }

    private void MoveHurt_OnEnd()
    {
        m_moveMode = MoveMode.NONE;
        m_hurtSource = null;
        rigidbody.velocity = Vector2.zero;
        DOSpriteAnimation(animationConfig.idle_Anim);
    }
    #endregion
}
