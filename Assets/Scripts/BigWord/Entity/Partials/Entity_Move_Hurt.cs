using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Entity
{
    /// <summary>
    /// 硬直时间
    /// </summary>
    private float m_hitRecoverTime;
    /// <summary>
    /// 是否处于硬直
    /// </summary>
    public bool isHitRecover { get { return m_hitRecoverTime <= 0f; } }
    /// <summary>
    /// 是否在倒地
    /// </summary>
    private bool m_fallDown;
    public bool fallDown { get { return m_fallDown; } }
    /// <summary>
    /// 是否浮空
    /// </summary>
    private bool m_airborne;
    public bool airborne { get { return m_airborne; } }
    /// <summary>
    /// 受击X方向速度
    /// </summary>
    private float m_hitVelocityX;
    /// <summary>
    /// 受击Y方向速度
    /// </summary>
    private float m_hitVelocityY;
    /// <summary>
    /// 受击初始Y方向速度（不会进行递减）
    /// </summary>
    private float m_inithitVelocityY;
    /// <summary>
    /// 反弹次数
    /// </summary>
    private int m_reboundTime;
    /// <summary>
    /// 倒地重新站立的时间
    /// </summary>
    private float m_standTime;

    #region 受击移动
    private void Move_UpdateHurt(float fixedDeltaTime)
    {
        //空中受击硬直减少
        m_hitRecoverTime -= airborne ? fixedDeltaTime * 2 : fixedDeltaTime;

        if (m_hurtSource != null && m_moveMode == MoveMode.HURT)
        {
            Move_UpdateHurt_X(fixedDeltaTime);
            Move_UpdateHurt_Y(fixedDeltaTime);
            m_hitVelocityX = m_hitVelocityX * -curFlip;
            rigidbody.velocity = new Vector2(m_hitVelocityX, 0);

            //没浮空 硬直消失后回复状态
            if (m_hitRecoverTime <= 0f && m_hitVelocityY == 0f && !m_airborne && !m_fallDown)
                MoveHurt_OnEnd();
        }

        MoveHurt_UpdateStand(fixedDeltaTime);

    }

    /// <summary>
    /// X轴上的计算
    /// </summary>
    /// <param name="fixedDeltaTime"></param>
    private void Move_UpdateHurt_X(float fixedDeltaTime)
    {
        if (Mathf.Abs(m_hitVelocityX) > 0)
        {
            m_hitVelocityX *= CommonUtility.AirFriction * m_hurtSource.acceleration * fixedDeltaTime;
        }
    }

    /// <summary>
    /// Y轴上的计算
    /// </summary>
    /// <param name="fixedDeltaTime"></param>
    private void Move_UpdateHurt_Y(float fixedDeltaTime)
    {
        m_airborne = skinNode.localPosition.y > 0;
        if (Mathf.Abs(m_inithitVelocityY) > 0 && m_hitRecoverTime <= 0.1f)
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
                if (Mathf.Abs(m_hitVelocityY) >= 5f && m_reboundTime < 1)//落地速度足够 反弹
                {
                    m_reboundTime++;
                    m_hitVelocityY = Mathf.Abs(m_hitVelocityY) * 0.5f;
                }
                else if (m_hitVelocityY < 0f)
                {
                    m_hitVelocityY = 0f;
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
            DOSpriteAnimation(animationConfig.sit_Anim);
            m_fallDown = false;
            animationFinish = MoveHurt_OnEnd;
        }
    }

    /// <summary>
    /// 受击动画开始
    /// </summary>
    /// <param name="damage"></param>
    private void MoveHurt_OnStart(DamageData damage)
    {
        m_hurtSource = damage;
        Move_Stop();
        m_standTime = 0f;
        animationFinish = null;
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
            if (m_fallDown || m_reboundTime > 0)
            {
                height *= 0.2f;
                //扫地，延长倒地站立时间
                m_standTime += m_hitRecoverTime;
            }
            else if (m_airborne)
                height *= skinNode.localPosition.y <= 0.65f ? 0.6f : 0.65f;

            
            m_inithitVelocityY = m_hitVelocityY = m_airborne && height == 0f ? m_hitVelocityY : Mathf.Sqrt(CommonUtility.GravitationalAcceleration * Mathf.Pow(height, 2));
            
            m_hitVelocityX = Mathf.Abs(m_inithitVelocityY) > 0f ? damage.velocityXY : m_hurtSource.velocityX; //浮空时X轴速度使用XY的配置

            //空中受击使用固定动画
            int index = m_inithitVelocityY > 0 ? 0 : Random.Range(0, animationConfig.HitAnim.Count);
            DOSpriteAnimation(animationConfig.HitAnim[index]);
        }

        m_moveMode = MoveMode.HURT;
    }

    /// <summary>
    /// 受击动画结束
    /// </summary>
    private void MoveHurt_OnEnd()
    {
        m_reboundTime = 0;
        m_moveMode = MoveMode.NONE;
        m_hurtSource = null;
        rigidbody.velocity = Vector2.zero;
        DOSpriteAnimation(animationConfig.idle_Anim);
    }
    #endregion
}
