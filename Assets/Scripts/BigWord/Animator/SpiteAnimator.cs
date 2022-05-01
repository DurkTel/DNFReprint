using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class SpiteAnimator : MonoBehaviour
{
    /// <summary>
    /// 是否暂停动画
    /// </summary>
    private bool m_pause;
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

    private List<RenenderSprite> m_renenderSprites = new List<RenenderSprite>();

    private AnimationData m_next_animationData;

    public AnimationData current_animationData;

    public AnimationData last_animationData;

    public AnimationConfig animationConfig;

    public UnityAction<int> updateSpriteEvent;

    public UnityAction<AnimationData> updateAnimationEvent;

    public UnityAction animationFinish;

    public int curFlip { get { return m_renenderSprites.Count > 0 ? m_renenderSprites[0].GetCurFlip() : 0; } }

    public void Start()
    {
        m_renenderSprites = GetComponentsInChildren<RenenderSprite>().ToList();
    }

    /// <summary>
    /// 每帧刷新动画
    /// </summary>
    public void TickSpriteAnimation(float deltaTime)
    {
        if (m_pause || current_animationData == null) return;
        List<AnimationFrameData> aniSprites = current_animationData.frameList;
        AnimationFrameData curSprite = aniSprites[m_currentFrame];

        m_totalTime += deltaTime;
        if (m_totalTime >= curSprite.interval / current_animationData.speed)
        {
            m_totalTime = 0;

            m_currentIndex = int.Parse(curSprite.sprite.name);
            SetSprites(m_currentIndex);

            m_lastFrame = m_currentFrame;

            updateSpriteEvent?.Invoke(m_lastFrame);
            m_currentFrame++;

            if (m_currentFrame >= aniSprites.Count)
            {
                if (animationFinish != null)
                {
                    animationFinish.Invoke();
                    animationFinish = null;
                }
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

        }
    }

    private void FixedUpdate()
    {
        TickSpriteAnimation(Time.fixedDeltaTime);
    }

    private void SetSprites(int index)
    {
        for (int i = 0; i < m_renenderSprites.Count; i++)
        {
            m_renenderSprites[i].SetSprite(index);
        }
    }

    public void PauseAni(bool pause)
    {
        m_pause = pause;
    }

    public void PlayHurtAnimation()
    {
        if (animationConfig.HitAnim.Count > 0)
        {
            DOSpriteAnimation(animationConfig.HitAnim[Random.Range(0, animationConfig.HitAnim.Count)]);
        }
    }

    public void PlayAirHurtAnimation()
    {
        if (animationConfig.HitAnim.Count > 0)
        {
            DOSpriteAnimation(animationConfig.hit2_Anim);
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
        last_animationData = current_animationData;
        current_animationData = animationData;
        updateAnimationEvent?.Invoke(animationData);
        m_totalTime = 999;
        m_currentFrame = 0;
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
}
