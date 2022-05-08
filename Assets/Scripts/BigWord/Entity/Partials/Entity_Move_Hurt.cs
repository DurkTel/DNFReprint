using cfg.db;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class Entity
{
    /// <summary>
    /// 硬直时间
    /// </summary>
    private float m_hitRecoverTime;
    /// <summary>
    /// 是否处于硬直
    /// </summary>
    public bool isHitRecover { get; set; }
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
    /// 伤害源
    /// </summary>
    private DamageCfg m_hurtSource;
    /// <summary>
    /// 受击Y方向速度
    /// </summary>
    private float m_hitVelocityY;
    /// <summary>
    /// 受击初始Y方向速度（不会进行递减）
    /// </summary>
    private float m_inithitVelocityY;
    /// <summary>
    /// 倒地重新站立的时间
    /// </summary>
    private float m_standTime;
    /// <summary>
    /// 加速度
    /// </summary>
    private float m_acceleration;
    /// <summary>
    /// 是否可以反弹
    /// </summary>
    private bool m_rebound;
    /// <summary>
    /// 计算的受击速度
    /// </summary>
    private Vector2 m_passiveSpeed_Rigi;
    /// <summary>
    /// 是否开始下落
    /// </summary>
    private bool m_enableDropDown;
    /// <summary>
    /// X轴打击结束回调
    /// </summary>
    private UnityAction m_callBack_MoveHurtX;
    /// <summary>
    /// X轴打击事件
    /// </summary>
    private UnityAction m_moveHurtX;
    /// <summary>
    /// Y轴打击事件
    /// </summary>
    private UnityAction m_moveHurtXY;
    /// <summary>
    /// 受击X方向速度
    /// </summary>
    private float m_hitVelocityX;
    /// <summary>
    /// 使用空中X轴速度
    /// </summary>
    private bool updateAirMoveX;
    /// <summary>
    /// 空中X轴速度
    /// </summary>
    private float m_hitAirVelocityX;
    /// <summary>
    /// 跳跃被击下落
    /// </summary>
    private UnityAction m_flyStandUp;
    /// <summary>
    /// 起立事件
    /// </summary>
    private UnityAction m_standUp;
    /// <summary>
    /// 打击列表
    /// </summary>
    public List<UnityAction> physicsEvent_HitStop = new List<UnityAction>();

    #region 受击移动
    private void Move_UpdateHurt(float fixedDeltaTime)
    {
        if (m_hitRecoverTime > 0)
        {
            m_hitRecoverTime -= fixedDeltaTime;
            isHitRecover = true;
        }
        else
        {
            isHitRecover = false;
        }

        for (int i = 0; i < physicsEvent_HitStop.Count; i++)
        {
            physicsEvent_HitStop[i]?.Invoke();
        }

        rigidbody.velocity = m_passiveSpeed_Rigi;


        if (isHitRecover || m_haltFrame > 0) return;

        skinNode.localPosition += Vector3.up * fixedDeltaTime * m_hitVelocityY;

        if (skinNode.localPosition.y > 0)
        {
            if ((m_jumpState != JumpState.NONE && m_enableDropDown == true) || m_jumpState == JumpState.NONE)
            {
                if (m_hitVelocityY > 0)
                {
                    m_hitVelocityY -= fixedDeltaTime * CommonUtility.GravitationalAcceleration * CommonUtility.AirFriction * 1;
                }
                else
                {
                    m_hitVelocityY -= fixedDeltaTime * CommonUtility.GravitationalAcceleration * CommonUtility.AirFriction * 1;
                }

            }
        }


        if (skinNode.localPosition.y <= 0 && m_rebound == false)
        {
            m_hitVelocityY = 0;
        }


        skinNode.localPosition = new Vector3(skinNode.localPosition.x, Mathf.Clamp(skinNode.localPosition.y, 0, Mathf.Infinity), skinNode.localPosition.z);

    }


    /// <summary>
    /// 受击开始
    /// </summary>
    /// <param name="damage"></param>
    private void MoveHurt_OnStart(DamageCfg damage, GameObject attacker = null)
    {
        m_hurtSource = damage;
        Move_Stop();
        //伤害来源是实体
        if (attacker != null)
        {
            //面对伤害源
            if (damage.LookAttacker)
            {
                float deltaX = attacker.transform.position.x - transform.position.x;
                SetSpriteFilp(deltaX < 0);
            }

            //施加X轴方向移动
            MoveHurt_X(m_hurtSource.VelocityX * -curFlip, m_hurtSource.Acceleration, m_hurtSource.RecoverTime);
            //施加Y轴方向移动
            MoveHurt_Y(m_hurtSource.HeightY, m_hurtSource.VelocityXY * -curFlip);
        }

        m_moveMode = MoveMode.HURT;

    }

    /// <summary>
    /// 受击动画结束
    /// </summary>
    private void MoveHurt_OnEnd()
    {
        m_moveMode = MoveMode.NONE;
        DOSpriteAnimation(animationConfig.idle_Anim);
        m_hurtSource = null;
        rigidbody.velocity = Vector2.zero;
    }
    #endregion

    #region X方向
    /// <summary>
    /// X轴帧刷新事件
    /// </summary>
    private void EVENT_MoveHurt_X()
    {
        //受击恢复后
        if (m_hitRecoverTime <= 0)
        {
            m_callBack_MoveHurtX?.Invoke();
            physicsEvent_HitStop.TryRemove(m_moveHurtX, (p) => { isHitRecover = false; });
            m_passiveSpeed_Rigi -= new Vector2(m_hitVelocityX, 0);
            m_hitVelocityX = 0;
            m_moveHurtX = null;
            if (physicsEvent_HitStop.Count <= 0)
                MoveHurt_OnEnd();

        }

        m_passiveSpeed_Rigi = new Vector2(m_hitVelocityX, 0);
        if (Mathf.Abs(m_hitVelocityX) > 0)
        {
            if (m_hitVelocityX > 0)
                m_hitVelocityX -= Time.deltaTime * m_acceleration * CommonUtility.AirFriction;
            else
            {
                m_hitVelocityX += Time.deltaTime * m_acceleration * CommonUtility.AirFriction;
            }
        }
    }

    /// <summary>
    /// 添加X轴的受击移动事件
    /// </summary>
    /// <param name="speed">速度</param>
    /// <param name="acceleration">加速度</param>
    /// <param name="hitRecoverTime">硬直时间</param>
    /// <param name="compulsive">强制</param>
    /// <param name="action">回调函数</param>
    private void MoveHurt_X(float speed, float acceleration, float hitRecoverTime = 0, bool compulsive = false, UnityAction action = null)
    {
        //初始化每帧调用的事件
        m_moveHurtX = EVENT_MoveHurt_X;
        m_acceleration = acceleration;
        if (physicsEvent_HitStop.Contains(m_flyStandUp))
        {
            physicsEvent_HitStop.TryRemove(m_flyStandUp);
            if (m_moveHurtXY == null)
                m_moveHurtXY = EVENT_MoveHurt_XY;
            physicsEvent_HitStop.TryUniqueAdd(m_moveHurtXY);
            m_enableDropDown = true;
        }

        //播放动画
        PlayHurtAnimation();

        //是否处于倒地状态
        if (!m_fallDown)
        {
            if (skinNode.transform.localPosition.y <= 0)
            {
                m_hitVelocityX = speed;
            }
            else
            {
                if (m_jumpState != JumpState.NONE)
                {
                    m_hitVelocityX = m_enableDropDown ? 0 : speed;
                }
                else
                {
                    m_hitVelocityX = 0;
                }
            }
        }
        else
        {
            //倒地位移减少
            m_hitVelocityX = speed * 0.3f;
        }

        m_callBack_MoveHurtX = action;
        if (hitRecoverTime > 0)
            isHitRecover = true;
        physicsEvent_HitStop.TryUniqueAdd(m_moveHurtX);

        if (physicsEvent_HitStop.Contains(m_moveHurtXY))
        {
            if (!compulsive)
                m_hitVelocityX = 0;

            m_rebound = true;
        }

        //空中受击
        if (skinNode.transform.localPosition.y > 0)
        {
            if ((m_enableDropDown && m_jumpState != JumpState.NONE) || m_jumpState == JumpState.NONE)
            {
                //播放空中受击动画
                PlayAirHurtAnimation();
                //空中僵硬时间减少
                m_hitRecoverTime = hitRecoverTime * 0.25f;
                if (m_moveHurtXY == null)
                    m_moveHurtXY = EVENT_MoveHurt_XY;
                physicsEvent_HitStop.TryUniqueAdd(m_moveHurtXY);
                return;
            }
        }

        //倒地受击
        if (m_fallDown)
        {
            //播放动画
            DOSpriteAnimation(animationConfig.airBorne_Anim);
            DoHitDown();
            //僵硬时间减少
            m_hitRecoverTime = hitRecoverTime * 0.5f;
            return;
        }

        m_hitRecoverTime = hitRecoverTime;
    }
    #endregion

    #region Y方向
    /// <summary>
    /// Y轴帧刷新事件
    /// </summary>
    private void EVENT_MoveHurt_XY()
    {
        //是否着地
        m_fallDown = skinNode.transform.localPosition.y > 0 ? false : true;

        if (m_hitVelocityY != 0)
        {
            //对于不同阶段播放不同的动画
            if (!m_fallDown)
            {
                if (m_hitVelocityY >= m_inithitVelocityY * 0.1f)
                {
                    if (m_rebound)
                        PlayAirHurtAnimation();
                    else
                        DOSpriteAnimation(animationConfig.airBorne_Anim);
                }
                if (m_hitVelocityY < m_inithitVelocityY * 0.1f)
                {

                    if (m_rebound)
                        DOSpriteAnimation(animationConfig.airBorne_Anim);
                    else
                        DOSpriteAnimation(animationConfig.rebound_Anim);
                }

            }
            else
            {
                if (m_hitVelocityY >= m_inithitVelocityY * 0.1f)
                {

                    DOSpriteAnimation(animationConfig.airBorne_Anim);
                }
                if (m_hitVelocityY < m_inithitVelocityY * 0.1f)
                {

                    if (m_rebound)
                        DOSpriteAnimation(animationConfig.airBorne_Anim);
                    else
                        DOSpriteAnimation(animationConfig.rebound_Anim);
                }
            }
        }

        //空中硬直
        if (isHitRecover) return;
        if (skinNode.transform.localPosition.y > 0)
        {
            if ((m_enableDropDown && m_jumpState != JumpState.NONE) || m_jumpState == JumpState.NONE)
            {
                updateAirMoveX = true;
                m_airborne = true;
            }
        }
        //用空中的X轴速度
        if (updateAirMoveX)
        {
            m_passiveSpeed_Rigi = new Vector2(m_hitAirVelocityX, m_passiveSpeed_Rigi.y);
        }

        if (skinNode.localPosition.y <= 0)
        {
            //落地速度少于5不反弹
            if (updateAirMoveX && Mathf.Abs(m_hitVelocityY) < 5)
            {
                StopAirBrone();
            }
            //可以反弹且落地速度大于5
            if (m_airborne)
            {
                if (Mathf.Abs(m_hitVelocityY) > 5)
                {
                    if (m_rebound)
                    {
                        m_hitVelocityY = Mathf.Abs(m_hitVelocityY) * 0.5f;
                        m_inithitVelocityY = m_hitVelocityY;
                        m_hitAirVelocityX *= 0.5f;
                        m_rebound = false;
                    }
                    else
                        StopAirBrone();
                }
            }
        }
    }

    /// <summary>
    /// 添加Y轴受击移动事件
    /// </summary>
    /// <param name="height">上升高度</param>
    /// <param name="speedX">速度</param>
    public void MoveHurt_Y(float height, float speedX)
    {
        if (physicsEvent_HitStop.Contains(m_flyStandUp))
        {
            physicsEvent_HitStop.Remove(m_flyStandUp);
            m_enableDropDown = true;
        }
        //可以反弹
        m_rebound = true;
        //记录初始速度
        m_inithitVelocityY = m_hitVelocityY;
        //空中的X轴速度
        m_hitAirVelocityX = speedX;

        if (m_moveHurtXY == null)
            m_moveHurtXY = EVENT_MoveHurt_XY;

        if (height > 0)
        {
            m_enableDropDown = true;
            physicsEvent_HitStop.TryUniqueAdd(m_moveHurtXY);
        }

        //不同情况下 对初速度进行处理
        if (m_airborne && skinNode.transform.localPosition.y <= 0.65f)
        {
            m_hitVelocityY = Mathf.Sqrt(CommonUtility.GravitationalAcceleration * Mathf.Pow(height * 0.6f, 2));
            return;
        }
        else if (m_fallDown)
        {
            m_hitVelocityY = Mathf.Sqrt(CommonUtility.GravitationalAcceleration * Mathf.Pow(height * 0.5f, 2));
            return;
        }

        m_hitVelocityY = Mathf.Sqrt(CommonUtility.GravitationalAcceleration * Mathf.Pow(height, 2));

    }

    /// <summary>
    /// 浮空落地
    /// </summary>
    private void StopAirBrone()
    {
        m_hitAirVelocityX = 0;
        m_passiveSpeed_Rigi = new Vector2(m_hitAirVelocityX, m_passiveSpeed_Rigi.y);
        physicsEvent_HitStop.TryRemove(m_moveHurtXY);
        m_passiveSpeed_Rigi -= new Vector2(m_hitAirVelocityX, m_passiveSpeed_Rigi.y);
        m_moveHurtXY = null;
        updateAirMoveX = false;
        m_airborne = false;
        DoHitDown();
    }


    #endregion

    #region 倒地
    private void EVENT_FlyStandUp()
    {
        skinNode.transform.localPosition += Vector3.up * Time.fixedDeltaTime * 2;
        if (skinNode.transform.localPosition.y >= 0)
        {
            physicsEvent_HitStop.TryRemove(m_flyStandUp);
            m_flyStandUp = null;
        }
        skinNode.transform.localPosition = new Vector3(skinNode.transform.localPosition.x, Mathf.Clamp(skinNode.transform.localPosition.y, 0, 0), skinNode.transform.localPosition.z);
    }


    /// <summary>
    /// 站立事件
    /// </summary>
    private void EVENT_MoveHurt_StanUp()
    {
        if (m_standTime > 0 && skinNode.localPosition.y <= 0 && !isHitRecover && m_fallDown)
        {
            m_standTime -= Time.fixedDeltaTime;
            if (m_standTime > 1 * 1 / 5f)
            {
                DOSpriteAnimation(animationConfig.lie_Anim);
            }
            if (m_standTime < 1 * 1 / 5f)
            {
                DOSpriteAnimation(animationConfig.sit_Anim);
                if (m_standTime <= 0)
                {
                    m_fallDown = false;
                    if (m_jumpState != JumpState.NONE)
                    {
                        m_enableDropDown = false;
                        if (m_flyStandUp == null)
                            m_flyStandUp += EVENT_FlyStandUp;
                        physicsEvent_HitStop.TryUniqueAdd(m_flyStandUp);
                    }

                    m_standTime = 0;
                    m_hitVelocityY = 0;
                    physicsEvent_HitStop.TryRemove(m_standUp);
                    m_standUp = null;
                    MoveHurt_OnEnd();
                }
            }
        }
    }

    /// <summary>
    /// 倒地
    /// </summary>
    private void DoHitDown()
    {
        m_fallDown = true;
        m_standTime = 1f;
        m_standUp = EVENT_MoveHurt_StanUp;
        physicsEvent_HitStop.TryUniqueAdd(m_standUp);
    }
    #endregion
}
