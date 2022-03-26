using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


public partial class Entity
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

    private List<RenenderSprite> m_renenderSprites = new List<RenenderSprite>();

    private EntityMotor m_motor;

    private AnimationData m_next_animationData;

    public AnimationData current_animationData;

    public AnimationData last_animationData;

    public AnimationConfig AnimationConfig;

    public UnityAction<int> UpdateSpriteEvent;

    public UnityAction<AnimationData> UpdateAnimationEvent;

    public InputReader inputReader;

    public int curFlip { get { return m_renenderSprites[0].GetCurFlip(); } }

    /// <summary>
    /// 每帧刷新动画
    /// </summary>
    public void TickSpriteAnimation(float deltaTime)
    {
        if (m_stop || current_animationData == null) return;
        List<AnimationFrameData> aniSprites = current_animationData.frameList;
        AnimationFrameData curSprite = aniSprites[m_currentFrame];

        m_totalTime += deltaTime;
        if (m_totalTime >= curSprite.interval / current_animationData.speed)
        {
            m_totalTime = 0;
            
            m_currentIndex = int.Parse(curSprite.sprite.name);
            SetSprites(m_currentIndex);
            
            m_lastFrame = m_currentFrame;

            UpdateSpriteEvent?.Invoke(m_lastFrame);
            m_currentFrame++;

            if (m_currentFrame >= aniSprites.Count)
            {
                if (m_next_animationData != null)
                {
                    DOSpriteAnimation(m_next_animationData);
                    m_next_animationData = null;
                }
                else if (current_animationData.isLoop)
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
                if (curSprite.frameEvent.Count > 0)
                {
                    foreach (var item in curSprite.frameEvent)
                    {
                        DoAnimFrameEvent(item);
                    }
                }
            }

        }
    }

    private void UpdateAnimation(float deltaTime)
    {
        TickSpriteAnimation(deltaTime);

        ConditionRelation(m_lastFrame);
    }

    private void SetSprites(int index)
    {
        for (int i = 0; i < m_renenderSprites.Count; i++)
        {
            m_renenderSprites[i].SetSprite(index);
        }
    }


    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="animationData">要播放的动画</param>
    public void DOSpriteAnimation(AnimationData animationData)
    {
        if (animationData == null)
        {
            Debug.LogError("动画数据为空");
            return;
        }
        //if (current_animationData == animationData) return;
        last_animationData = current_animationData;
        current_animationData = animationData;
        UpdateAnimationEvent?.Invoke(animationData);
        m_totalTime = 999;
        m_currentFrame = 0;
        m_isFirstList = new bool[animationData.frameList.Count];
    }

    /// <summary>
    /// 播放完当前动画后播放该动画
    /// </summary>
    /// <param name="animationData">要播放的动画</param>
    public void DOSpriteAnimationNext(AnimationData animationData)
    {
        if (animationData == null)
        {
            Debug.LogError("动画数据为空");
            return;
        }
        m_next_animationData = animationData;
    }

    /// <summary>
    /// 强制播放技能动画
    /// </summary>
    /// <param name="animationData">要切换的技能动画</param>
    /// <param name="onlyOnCommon">是否只在移动、普通攻击时强制</param>
    public void ForceDOSkillAnimation(AnimationData animationData, bool onlyOnCommon = true)
    {
        if (!onlyOnCommon || !IsInThisAni(AnimationConfig.ForceAnim))
            return;
        DOSpriteAnimation(animationData);
    }

    /// <summary>
    /// 强制播放技能动画
    /// </summary>
    /// <param name="animationData">要切换的技能动画</param>
    /// <param name="onlyOnCommon">是否只在移动、普通攻击时强制</param>
    public void ForceDOSkillAnimation(string animationDataName, CommonDefine.Career career, bool onlyOnCommon = true)
    {
        if (!onlyOnCommon || !IsInThisAni(AnimationConfig.ForceAnim))
            return;

        string path = string.Format("{0}Character/Player/{1}/skill/{2}.asset", CommonDefine.AnimationDataAssetPath ,(int)career, animationDataName);
        AnimationData animationData = AssetDatabase.LoadAssetAtPath(path, typeof(AnimationData)) as AnimationData;
        if (!animationData) return;

        DOSpriteAnimation(animationData);
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

    /// <summary>
    /// 当前播放的是否是这个动画
    /// </summary>
    /// <param name="animationData">要检测的动画</param>
    /// <returns></returns>
    public bool IsInThisAni(AnimationData animationData)
    {
        FieldInfo[] fieldInfos = typeof(AnimationConfig).GetFields();

        return current_animationData == animationData;
    }

    /// <summary>
    /// 当前播放的是否是这个动画
    /// </summary>
    /// <param name="baseAnim">动画类型</param>
    /// <returns></returns>
    public bool IsInThisAni(List<AnimationData> animations)
    {
        return animations.Contains(current_animationData);
    }

    /// <summary>
    /// 执行动画事件
    /// </summary>
    /// <param name="frameEvent"></param>
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
                                        if (skinNode.localPosition.y <= item.spritePostionY.targetValue) result = false;

                                        break;
                                    case Condition.Relation.smaller:
                                        if (skinNode.localPosition.y >= item.spritePostionY.targetValue) result = false;

                                        break;
                                    case Condition.Relation.equal:
                                        if (skinNode.localPosition.y != item.spritePostionY.targetValue) result = false;

                                        break;
                                    case Condition.Relation.bigger_E:
                                        if (skinNode.localPosition.y < item.spritePostionY.targetValue) result = false;

                                        break;
                                    case Condition.Relation.smaller_E:
                                        if (skinNode.localPosition.y > item.spritePostionY.targetValue) result = false;

                                        break;
                                    case Condition.Relation.unequal:
                                        if (skinNode.localPosition.y == item.spritePostionY.targetValue) result = false;

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
                                if (inputReader != null)
                                {
                                    ButtonBehaviour buttonBehaviour = inputReader.buttonBehaviour[CharacterEventFunc.GetInputEnumToString(item.inputComparison.action)];
                                    switch (item.inputComparison.inputType)
                                    {
                                        case Condition.InputType.onPressed:
                                            if (!buttonBehaviour.onPressed) result = false;

                                            break;

                                        case Condition.InputType.onReleased:
                                            if (!buttonBehaviour.onRelease) result = false;

                                            break;
                                        case Condition.InputType.multiPressed:
                                            if (!buttonBehaviour.onMulti) result = false;

                                            break;
                                        case Condition.InputType.holdPressed:
                                            if (!buttonBehaviour.onHold) result = false;

                                            break;
                                        default:
                                            break;
                                    }
                                }
                                else
                                {
                                    result = false;
                                }
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
                                        if (m_motor.movePhase != 1) result = false;

                                        break;
                                    case CustomCondition.RUN_LIMIT:
                                        if (m_motor.movePhase != 2) result = false;

                                        break;
                                    case CustomCondition.JUMP_ATTACK_LIMIT:
                                        CharactMotor charactMotor = (CharactMotor)m_motor;
                                        if (!skillManager.characterSkillTree.IsHasSkill(10001) && charactMotor.airAttackCombo > 0) result = false;
                                        //result = false;
                                        break;
                                    case CustomCondition.HIT_RECOVER:
                                        if (m_motor.isHitRecover) result = false;
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
