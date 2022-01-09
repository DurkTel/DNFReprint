using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IAnimatorEvent
{

}

public class AnimationEvent : IAnimatorEvent
{
    public UnityAction unityAction;

    public AnimationEvent(UnityAction action)
    {
        unityAction += action;
    }
}

public class AnimationEvent<T> : IAnimatorEvent
{
    public UnityAction<T> unityAction;

    public AnimationEvent(UnityAction<T> action)
    {
        unityAction += action;
    }
}
