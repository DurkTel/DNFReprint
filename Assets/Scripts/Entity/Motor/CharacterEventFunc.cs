using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;


public static class CharacterEventFunc
{
    /// <summary>
    /// 添加跳跃事件执行（Y轴的移动）
    /// </summary>
    /// <param name="transform">移动物体</param>
    /// <param name="power">移动Y值</param>
    /// <param name="duration">移动时长</param>
    /// <param name="callback">移动完成的回调</param>
    public static void DoAddJumpEvent(Transform transform, float power, float duration, UnityAction callback = null)
    {
        Tweener tweener = transform.DOLocalMoveY(power, duration);
        if (callback != null)
            tweener.OnComplete(() => callback.Invoke()); ;
    }
}
