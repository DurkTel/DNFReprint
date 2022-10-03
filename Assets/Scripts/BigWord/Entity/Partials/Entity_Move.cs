using AI;
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

    private float m_curSpeed;

    private float m_moveDirCoefficient = 1f;

    private float m_addMoveForce;

    private bool m_isHitAir;

    private bool m_inputEnabled;

    private Vector2 m_curMoveDir;

    private Vector2 m_velocity;

    private List<Vector2> m_movePathPosList = new List<Vector2>();

    private Vector2 m_moveStartPos;

    private float m_moveStartTime;

    private float m_moveSeed;

    private bool m_enableMoveInput = true;

    /// <summary>
    /// 0 没有移动 1 走路 2跑步
    /// </summary>
    public int movePhase;
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
        m_curMoveDir = m_enableMoveInput ? input : Vector2.zero;
    }

    protected void MovePhaseHandle(string action)
    {
        if (!m_inputEnabled || m_moveMode == MoveMode.HURT || status == EntityUnitily.PEACE)
            return;
        if (action == "Move")
            movePhase = 2;
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

    public void Set_MoveSeed(float moveSeed)
    {
        m_moveSeed = Mathf.Max(0, moveSeed);
    }

    public void Set_InputEnable(bool enable)
    {
        m_enableMoveInput = enable;
    }

    public void Add_MoveForce(float force)
    { 
        m_addMoveForce = force;
    }

    private void ReleaseMove()
    {
        m_moveMode = MoveMode.NONE;
        m_curSpeed = 0f;
        m_moveDirCoefficient = 1f;
        m_addMoveForce = 0f;
        m_isHitAir = false;
        m_inputEnabled = false;
        m_curMoveDir = m_velocity = m_moveStartPos = Vector2.zero;
        movePhase = 0;
        m_jumpState = JumpState.NONE;
        m_jumpSpeed = 0;
        m_movePathPosList.Clear();
        m_moveStartTime = 0f;
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

        if (IsInThisTagAni(AnimationMap.AniType.NOTMOVE))
            m_curMoveDir = Vector2.zero;
       
        Vector2 dir = m_curMoveDir;
        m_curSpeed = movePhase == 1 ? m_moveSeed : m_moveSeed * 1.8f;
        m_curSpeed = m_jumpState == JumpState.NONE ? m_curSpeed : 1.2f; //跳跃固定位置速度
        m_velocity = dir * m_curSpeed * m_moveDirCoefficient;
        rigidbody.velocity = m_velocity;
        
        bool onAttack = IsInThisTagAni(AnimationMap.AniType.ATTACK);

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

    #endregion

    #region 路径移动

    public void Move_NavigationPath(List<PathNode> path)
    {
        if (m_jumpState != JumpState.NONE || m_moveMode == MoveMode.HURT)
            return;

        if (path.Count < 1)
            return;

        if (m_movePathPosList.Count > 1)
            m_movePathPosList.Clear();

        for (int i = 0; i < path.Count; i++)
        {
            PathNode node = path[i];
            m_movePathPosList.Add(new Vector2(0.1f * node.X, 0.1f * node.Y));
        }

        m_moveMode = MoveMode.PATH;
        movePhase = 0;
    }

    public void MoveStop_NavigationPath()
    {
        m_movePathPosList.Clear();
        rigidbody.velocity = Vector2.zero;
        Move_OnEnd();
    }

    private void Move_UpdatePath(float fixedDeltaTime)
    {
        if (m_movePathPosList.Count < 1)
        {
            Move_OnEnd();
            return;
        }

        if (movePhase == 0)
        { 
            Move_OnStart();
            m_curSpeed = movePhase == 1 ? m_moveSeed : m_moveSeed * 2;
            m_moveStartTime = Time.realtimeSinceStartup;
        }

        m_moveStartPos = transform.localPosition;
        Vector2 ePos = m_movePathPosList[0];


        m_curMoveDir.x = ePos.x - m_moveStartPos.x > 0 ? 1f : -1f;
        m_curMoveDir.y = ePos.y - m_moveStartPos.y > 0 ? 1f : -1f;
        //目标距离
        float dis = Vector3.Distance(m_moveStartPos, ePos); 

        if(dis < 0.01f)
        {
            m_movePathPosList.RemoveAt(0);
            return;
        }

        float curTime = Time.realtimeSinceStartup;
        //已移动距离
        float movedDis = (curTime - m_moveStartTime) * m_curSpeed;
        float lerp = movedDis / dis;

        Vector3 nextPos = Vector3.Lerp(m_moveStartPos, ePos, lerp);
        m_moveStartPos = nextPos;
        m_moveStartTime = curTime;

        //移动到了目标点
        if (lerp >= 1f)
            m_movePathPosList.RemoveAt(0);

        transform.localPosition = nextPos;
        Move_OnUpdate();
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
                Move_UpdatePath(fixedDeltaTime);
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
    private void Move_ForceX(float fixedDeltaTime)
    {

        if (Mathf.Abs(m_addMoveForce) > 0.1)
        {
            rigidbody.AddForce(Vector2.right * m_addMoveForce * curFlip, ForceMode2D.Impulse);
            m_addMoveForce = 0f;
        }

    }

    public void SetSpriteFilp(bool isLeft)
    {
        foreach (var item in m_renenderSprites)
        {
            item.SetSpriteFilp(isLeft);
        }
    }

    private bool FilpLimit()
    {
        bool attackAnimLimit = !IsInThisTagAni(AnimationMap.AniType.ATTACK) && !IsInThisTagAni(AnimationMap.AniType.NOTMOVE);
        return attackAnimLimit;
    }
    #endregion
}
