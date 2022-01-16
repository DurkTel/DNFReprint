using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class SpriteAnimator : MonoBehaviour
{
    [HideInInspector]
    public bool m_stop;
    /// <summary>
    /// 每帧使用时间
    /// </summary>
    private float m_totalTime = 999;
    /// <summary>
    /// 当前帧动画进行到的那一帧
    /// </summary>
    private int m_currentFrame;
    /// <summary>
    /// 当前帧动画进行到的上一帧
    /// </summary>
    private int m_lastFrame;
    /// <summary>
    /// 动画配置的下标
    /// </summary>
    private int m_currentIndex;

    private bool[] m_isFirstList;

    private RenenderSprite m_renenderSprite;

    private Transform m_charactRenderer;

    private EntityMotor m_motor;

    public AnimationData current_animationData;

    public AnimationData last_animationData;

    public AnimationConfig AnimationConfig;

    public UnityAction<int> UpdateSpriteEvent;

    public UnityAction<AnimationData> UpdateAnimationEvent;


    private void Start()
    {
        m_renenderSprite = GetComponent<RenenderSprite>();
        m_charactRenderer = GetComponent<Transform>();
        m_motor = GetComponentInParent<EntityMotor>();
    }

    private void Update()
    {
        TickSpriteAnimation();
        ConditionRelation(m_lastFrame);

    }

    //private void FixedUpdate()
    //{

    //}

    public void TickSpriteAnimation()
    {
        if (m_stop || current_animationData == null) return;
        List<AnimationFrameData> aniSprites = current_animationData.frameList;
        AnimationFrameData curSprite = aniSprites[m_currentFrame];

        m_totalTime += Time.deltaTime;
        if (m_totalTime >= curSprite.interval / current_animationData.speed)
        {
            m_totalTime = 0;
            
            m_currentIndex = int.Parse(curSprite.sprite.name);
            m_renenderSprite.SetSprite(m_currentIndex);
            
            m_lastFrame = m_currentFrame;

            UpdateSpriteEvent?.Invoke(m_lastFrame);
            m_currentFrame++;

            if (m_currentFrame >= aniSprites.Count)
            {
                if (current_animationData.isLoop)
                {
                    m_currentFrame = 0;
                }
                else
                {
                    m_currentFrame = aniSprites.Count - 1;
                }
            }

            if (curSprite.frameEventLoop || !m_isFirstList[m_lastFrame])
            {
                m_isFirstList[m_lastFrame] = true;
                DoAnimFrameEvent(curSprite.frameEvent);
            }

        }
    }

    public void DOSpriteAnimation(AnimationData animationData)
    {
        if (animationData == null)
        {
            Debug.LogError("动画数据为空");
            return;
        }
        if (current_animationData == animationData) return;
        last_animationData = current_animationData;
        current_animationData = animationData;
        UpdateAnimationEvent?.Invoke(animationData);
        m_totalTime = 999;
        m_currentFrame = 0;
        m_isFirstList = new bool[animationData.frameList.Count];
    }

    public float GetCurAnimationLength()
    {
        if (current_animationData == null) return 0;

        float totalLength = 0;
        for (int i = 0; i < current_animationData.frameList.Count; i++)
        {
            totalLength += current_animationData.frameList[i].interval;
        }

        return totalLength;
    }

    public bool IsInThisAni(AnimationData animationData)
    {
        return current_animationData == animationData;
    }

    public bool IsInThisAni(IBaseAnim baseAnim)
    {
        FieldInfo[] fieldInfos = baseAnim.GetType().GetFields();

        foreach (var item in fieldInfos)
        {
            if (item.Name == current_animationData.aniName)
                return true;
        }

        return false;
    }

    private void DoAnimFrameEvent(FrameEvent frameEvent)
    {
        switch (frameEvent.paramType)
        {
            case EventParamDefine.None:
                m_motor.OnAnimEvent(frameEvent.eventType);
                break;
            case EventParamDefine.Bool:
                m_motor.OnAnimEvent(frameEvent.eventType, frameEvent.parameterBool);
                break;
            case EventParamDefine.Int:
                m_motor.OnAnimEvent(frameEvent.eventType, frameEvent.parameterInt);
                break;
            case EventParamDefine.Float:
                m_motor.OnAnimEvent(frameEvent.eventType, frameEvent.parameterFloat);
                break;
            case EventParamDefine.String:
                m_motor.OnAnimEvent(frameEvent.eventType, frameEvent.parameterString);
                break;
            default:
                break;
        }


    }

    /// <summary>
    /// 自动切换动画的条件判断
    /// </summary>
    /// <param name="frame"></param>
    /// <returns></returns>
    private bool ConditionRelation(int frame)
    {
        if (current_animationData == null) return false;
        bool result = false;
        if (current_animationData.switchingConditions.Count > 0)
        {
            for (int i = 0; i < current_animationData.switchingConditions.Count; i++)
            {
                SwitchingConditions curFrameCondition = current_animationData.switchingConditions[i];
                if (curFrameCondition.frame.x <= frame && curFrameCondition.frame.y >= frame || curFrameCondition.frame == Vector2Int.zero)
                {
                    result = true;

                    foreach (var item in curFrameCondition.conditions)
                    {
                        switch (item.conditionType)
                        {
                            case Condition.ConditionType.animation:
                                if (current_animationData != item.characterAnimation) result = false;
                                break;
                            case Condition.ConditionType.spritePostionY:
                                switch (item.spritePostionY.relation)
                                {
                                    case Condition.Relation.bigger:
                                        if (m_charactRenderer.localPosition.y <= item.spritePostionY.targetValue) result = false;

                                        break;
                                    case Condition.Relation.smaller:
                                        if (m_charactRenderer.localPosition.y >= item.spritePostionY.targetValue) result = false;

                                        break;
                                    case Condition.Relation.equal:
                                        if (m_charactRenderer.localPosition.y != item.spritePostionY.targetValue) result = false;

                                        break;
                                    case Condition.Relation.bigger_E:
                                        if (m_charactRenderer.localPosition.y < item.spritePostionY.targetValue) result = false;

                                        break;
                                    case Condition.Relation.smaller_E:
                                        if (m_charactRenderer.localPosition.y > item.spritePostionY.targetValue) result = false;

                                        break;
                                    case Condition.Relation.unequal:
                                        if (m_charactRenderer.localPosition.y == item.spritePostionY.targetValue) result = false;

                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case Condition.ConditionType.spriteSpeedY:
                                switch (item.spriteSpeedY.relation)
                                {
                                    case Condition.Relation.bigger:
                                        if (m_motor.speedDrop <= item.spriteSpeedY.targetValue) result = false;

                                        break;
                                    case Condition.Relation.smaller:
                                        if (m_motor.speedDrop >= item.spriteSpeedY.targetValue) result = false;

                                        break;
                                    case Condition.Relation.equal:
                                        if (m_motor.speedDrop != item.spriteSpeedY.targetValue) result = false;

                                        break;
                                    case Condition.Relation.bigger_E:
                                        if (m_motor.speedDrop < item.spriteSpeedY.targetValue) result = false;

                                        break;
                                    case Condition.Relation.smaller_E:
                                        if (m_motor.speedDrop > item.spriteSpeedY.targetValue) result = false;

                                        break;
                                    case Condition.Relation.unequal:
                                        if (m_motor.speedDrop == item.spriteSpeedY.targetValue) result = false;

                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case Condition.ConditionType.inputSpeedX:
                                switch (item.inputSpeedX.relation)
                                {
                                    case Condition.Relation.bigger:
                                        if (m_motor.curMoveDir.x <= item.inputSpeedX.targetValue) result = false;

                                        break;
                                    case Condition.Relation.smaller:
                                        if (m_motor.curMoveDir.x >= item.inputSpeedX.targetValue) result = false;

                                        break;
                                    case Condition.Relation.equal:
                                        if (m_motor.curMoveDir.x != item.inputSpeedX.targetValue) result = false;

                                        break;
                                    case Condition.Relation.bigger_E:
                                        if (m_motor.curMoveDir.x < item.inputSpeedX.targetValue) result = false;

                                        break;
                                    case Condition.Relation.smaller_E:
                                        if (m_motor.curMoveDir.x > item.inputSpeedX.targetValue) result = false;

                                        break;
                                    case Condition.Relation.unequal:
                                        if (m_motor.curMoveDir.x == item.inputSpeedX.targetValue) result = false;

                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case Condition.ConditionType.inputSpeedY:
                                switch (item.inputSpeedY.relation)
                                {
                                    case Condition.Relation.bigger:
                                        if (m_motor.curMoveDir.y <= item.inputSpeedY.targetValue) result = false;

                                        break;
                                    case Condition.Relation.smaller:
                                        if (m_motor.curMoveDir.y >= item.inputSpeedY.targetValue) result = false;

                                        break;
                                    case Condition.Relation.equal:
                                        if (m_motor.curMoveDir.y != item.inputSpeedY.targetValue) result = false;

                                        break;
                                    case Condition.Relation.bigger_E:
                                        if (m_motor.curMoveDir.y < item.inputSpeedY.targetValue) result = false;

                                        break;
                                    case Condition.Relation.smaller_E:
                                        if (m_motor.curMoveDir.y > item.inputSpeedY.targetValue) result = false;

                                        break;
                                    case Condition.Relation.unequal:
                                        if (m_motor.curMoveDir.y == item.inputSpeedY.targetValue) result = false;

                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case Condition.ConditionType.inputKey:
                                if (Input.anyKey)
                                {
                                    switch (item.inputComparison.inputType)
                                    {
                                        case Condition.InputType.onPressed:
                                            if (!KeyboardInput.Instance.GetButtonFormKeyEnum(item.inputComparison.keyCode).OnPressed) result = false;
                                            
                                            break;
                                        case Condition.InputType.onReleased:
                                            if (!KeyboardInput.Instance.GetButtonFormKeyEnum(item.inputComparison.keyCode).OnReleased) result = false;

                                            break;
                                        case Condition.InputType.doublePressed:
                                            if (!(KeyboardInput.Instance.GetButtonFormKeyEnum(item.inputComparison.keyCode).OnPressed && KeyboardInput.Instance.GetButtonFormKeyEnum(item.inputComparison.keyCode).IsExtending)) result = false;

                                            break;
                                        case Condition.InputType.isPressing:
                                            if (!KeyboardInput.Instance.GetButtonFormKeyEnum(item.inputComparison.keyCode).IsPressing) result = false;

                                            break;
                                        default:
                                            break;
                                    }
                                }
                                else
                                    result = false;
                                break;
                            case Condition.ConditionType.custom:
                                switch (item.customCondition)
                                {
                                    case CustomCondition.NONE:

                                        break;
                                    case CustomCondition.NO_MOVE_INPUT:
                                        if (m_motor.curMoveDir != Vector2.zero) result = false;

                                        break;
                                    case CustomCondition.WALK_LIMIT:
                                        if (!m_motor.walkingReady) result = false;

                                        break;
                                    case CustomCondition.RUN_LIMIT:
                                        if (!m_motor.runningReady) result = false;

                                        break;
                                    case CustomCondition.JUMP_ATTACK_LIMIT:
                                        CharactMotor charactMotor = (CharactMotor)m_motor;
                                        if (!CustomConditionFuc.IsStudiedThisSkill(10001, charactMotor.characterSkillTree) && charactMotor.airAttackCombo > 0) result = false;

                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    //print(result + "....." + i);
                    if (result)
                    { 
                        DOSpriteAnimation(curFrameCondition.animationData);
                        return true;
                    }
                }
            }
        }

        return result;

    }

}
