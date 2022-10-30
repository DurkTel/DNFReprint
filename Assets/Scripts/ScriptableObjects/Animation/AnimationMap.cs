using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Animation/AnimationMap")]
public class AnimationMap : ScriptableObject
{
    public List<string> names = new List<string>();

    public List<AnimationData> animations = new List<AnimationData>();

    public List<AniType> animationFlags = new List<AniType>();

    public void AddAnimation(string name, AnimationData animation)
    {
        //if (!names.Contains(name))
        //{
            names.Add(name);
            animations.Add(animation);
            animationFlags.Add(0);
        //}
    }

    public void RemoveAnimation(int index)
    {
        names.RemoveAt(index);
        animations.RemoveAt(index);
        animationFlags.RemoveAt(index);
    }

    private int GetIndex(string name)
    {
        for (int i = 0; i < names.Count; i++)
        {
            if (names[i].Equals(name))
                return i;
        }

        return -1;
    }

    /// <summary>
    /// 通过名字获取动画
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public AnimationData TryGetAnimation(string name)
    {
        int index = GetIndex(name);
        if (index != -1)
            return animations[index];

        return null;
    }

    /// <summary>
    /// 这个动画是否包含这个标签
    /// </summary>
    /// <param name="name"></param>
    /// <param name="aniType"></param>
    /// <returns></returns>
    public bool ContainsTag(string name, AniType aniType)
    { 
        int index = GetIndex(name);
        if (index != -1)
            return (animationFlags[index] & aniType) != 0;

        return false;
    }

    /// <summary>
    /// 这个动画是否包含这个标签
    /// </summary>
    /// <param name="name"></param>
    /// <param name="aniType"></param>
    /// <returns></returns>
    public bool ContainsTag(string name, int aniType)
    {
        int index = GetIndex(name);
        if (index != -1)
            return (animationFlags[index] & (AniType)aniType) != 0;

        return false;
    }

    /// <summary>
    /// 获取有该标签的所有动画
    /// </summary>
    /// <param name="aniType"></param>
    /// <returns></returns>
    public List<AnimationData> GetAnimationsByTag(AniType aniType)
    {
        List<AnimationData> list = new List<AnimationData>();

        for (int i = 0; i < animationFlags.Count; i++)
        {
            if ((animationFlags[i] & aniType) != 0 && animations[i])
                list.Add(animations[i]);
        }

        return list;
    }

    /// <summary>
    /// 获取有该标签的所有动画
    /// </summary>
    /// <param name="aniType"></param>
    /// <returns></returns>
    public List<AnimationData> GetAnimationsByTag(int aniType)
    {
        List<AnimationData> list = new List<AnimationData>();

        for (int i = 0; i < animationFlags.Count; i++)
        {
            if ((animationFlags[i] & (AniType)aniType) != 0 && animations[i])
                list.Add(animations[i]);
        }

        return list;
    }

    [System.Flags]
    public enum AniType
    { 
        COMMON = 1,
        JUMP = 2,
        ATTACK = 4,
        HURT = 8,
        FORCE = 16,
        NOTMOVE = 32,
        SKILL = 64,
    }

    public enum AnimationEnum
    { 
        IDLE_ANIM,
        IDLE_TOWN_ANIM,
        WALK_ANIM,
        RUN_ANIM,
        SIT_ANIM,
        AIR_BORNE_ANIM,
        REBOUND_ANIM,
        LIE_ANIM,
        JUMP_ANIM,
        JUMP_RISE_ANIM,
        JUMP_DROP_ANIM,
        JUMP_END_ANIM,
        JUMP_ATTACK_ANIM,
        ATTACK_1_ANIM,
        ATTACK_2_ANIM,
        ATTACK_3_ANIM,
        RUN_ATTACK_ANIM,
        HURT_1_ANIM,
        HURT_2_ANIM,
        HURT_3_ANIM,
        DEATH_ANIM,
    }
}
