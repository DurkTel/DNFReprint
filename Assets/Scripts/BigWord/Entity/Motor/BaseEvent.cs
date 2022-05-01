using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseEvent
{
    public Dictionary<EventDefine, IAnimatorEvent> eventDic = new Dictionary<EventDefine, IAnimatorEvent>();

    #region 单个对象的动画事件
    protected virtual void InitAnimEvent(EventDefine eventType, UnityAction unityAction)
    {
        if (eventDic.ContainsKey(eventType))
        {
            (eventDic[eventType] as AnimationEvent).unityAction += unityAction;
        }
        else
        {
            eventDic.Add(eventType, new AnimationEvent(unityAction));
        }
    }

    protected virtual void InitAnimEvent<T>(EventDefine eventType, UnityAction<T> unityAction)
    {
        if (eventDic.ContainsKey(eventType))
        {
            (eventDic[eventType] as AnimationEvent<T>).unityAction += unityAction;
        }
        else
        {
            eventDic.Add(eventType, new AnimationEvent<T>(unityAction));
        }
    }

    public void OnAnimEvent(EventDefine eventType)
    {
        if (eventDic.ContainsKey(eventType))
        {
            AnimationEvent action = (eventDic[eventType] as AnimationEvent);
            if (action == null)
            {
                Debug.LogError("action为空" + eventType.ToString());
                return;
            }
            action.unityAction?.Invoke();
        }
    }
    public void OnAnimEvent<T>(EventDefine eventType, T param)
    {
        if (eventDic.ContainsKey(eventType))
        {
            AnimationEvent<T> action = (eventDic[eventType] as AnimationEvent<T>);
            if (action == null)
            {
                Debug.LogError("action为空" + eventType.ToString());
                return;
            }
            action.unityAction?.Invoke(param);
        }
    }
    #endregion

}
