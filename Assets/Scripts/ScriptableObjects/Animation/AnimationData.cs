using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "ScriptableObject/Animation/AnimationData")]
public class AnimationData : ScriptableObject
{
    public string aniName;

    public bool isLoop;

    [Range(0.1f,3f)]
    public float speed = 1f;

    [SerializeField]
    public List<AnimationFrameData> frameList = new List<AnimationFrameData>();

    public List<SwitchingConditions> switchingConditions = new List<SwitchingConditions>();

}

[System.Serializable]
public class AnimationFrameData
{
    public Sprite sprite;

    public float interval = 0.1f;

    public bool frameEventLoop;

    public CharacterEventDefine eventType;

}

[System.Serializable]
public struct SwitchingConditions
{
    /// <summary>
    /// 值为-1时为任意帧，不为-1时为该帧
    /// </summary>
    public int frame;

    public AnimationData animationData;

    public List<Condition> conditions;
}

[System.Serializable]
public class Condition
{
    public enum ConditionType
    {
        animation,
        spritePostionY,
        spriteSpeedY,
        inputSpeedX,
        inputSpeedY,
        inputKey,
        custom
    }
    public enum Relation
    {
        bigger,
        smaller,
        equal,
        bigger_E,
        smaller_E,
        unequal,
    }

    public enum InputType
    {
        onPressed,
        onReleased,
        doublePressed,
        isPressing
    }

    [System.Serializable]
    public struct ValueComparison
    {
        public Relation relation;
        public float targetValue;
    }

    [System.Serializable]
    public struct InputComparison
    {
        public InputType inputType;
        public InputKeys keyCode;
    }

    public ConditionType conditionType;
    public AnimationData characterAnimation;
    public ValueComparison spritePostionY;
    public ValueComparison spriteSpeedY;
    public ValueComparison inputSpeedX;
    public ValueComparison inputSpeedY;
    public InputComparison inputComparison;
    public CustomCondition customCondition;
}




