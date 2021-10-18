using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpriteAnimator : MonoBehaviour
{

    public bool m_stop;

    private float m_totalTime = 999;

    private int m_currentFrame;

    private int m_lastFrame;

    private int m_currentIndex;

    private bool[] m_isFirstList;

    private RenenderSprite m_renenderSprite;

    private Transform m_charactRenderer;

    private BaseMotor m_motor;

    public AnimationData current_animationData;

    public AnimationData last_animationData;

    public AnimationConfig AnimationConfig;

    public UnityAction EVENT_AddJump;
    
    public UnityAction EVENT_AirAttackCombo;

    public UnityAction EVENT_SkillCombo;

    private void Start()
    {
        m_renenderSprite = GetComponent<RenenderSprite>();
        m_charactRenderer = GetComponent<Transform>();
        m_motor = GetComponentInParent<BaseMotor>();
    }

    private void Update()
    {
        TickSpriteAnimation();
        ConditionRelation(m_lastFrame);

    }

    private void FixedUpdate()
    {

    }

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
                DoAnimFrameEvent(curSprite);
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

    private void DoAnimFrameEvent(AnimationFrameData frameData)
    {
        switch (frameData.eventType)
        {
            case CharacterEventDefine.NONE:
                break;
            case CharacterEventDefine.ADD_JUMP_FORCE:
                EVENT_AddJump.Invoke();
                break;
            case CharacterEventDefine.AIR_ATTACK_COMBO:
                EVENT_AirAttackCombo.Invoke();
                break;
            case CharacterEventDefine.SKILL_COMBO:
                EVENT_SkillCombo.Invoke();
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
        bool result = false;
        if (current_animationData.switchingConditions.Count > 0)
        {
            for (int i = 0; i < current_animationData.switchingConditions.Count; i++)
            {
                if (current_animationData.switchingConditions[i].frame == frame || current_animationData.switchingConditions[i].frame == -1)
                {
                    result = true;

                    foreach (var item in current_animationData.switchingConditions[i].conditions)
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
                                break;
                            case Condition.ConditionType.inputSpeedY:
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
                                    case CustomCondition.JUMP_ATTACK_LIMIT:
                                        if (!CustomConditionFuc.IsStudiedThisSkill(10001, m_motor.characterSkillTree) && m_motor.airAttackCombo > 0) result = false;

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
                        DOSpriteAnimation(current_animationData.switchingConditions[i].animationData);
                }
            }
        }

        return result;

    }

}
