using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Animation/AnimationMap")]
public class AnimationMap : ScriptableObject
{
    public List<string> names;

    public List<AnimationData> animations;

    public List<AniType> aniType;

    public void AddAnimation(string name, AnimationData animation)
    {
        if (!names.Contains(name))
        {
            names.Add(name);
            animations.Add(animation);
            aniType.Add(AniType.COMMON);
        }
    }

    public bool RemoveAnimation(string name)
    {
        int index = GetIndex(name);
        if (index != -1)
        {
            names.RemoveAt(index);
            animations.RemoveAt(index);
        }

        return false;
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

    [System.Flags]
    public enum AniType
    { 
        COMMON = 1,
        JUMP = 2,
        ATTACK = 4,
        HURT = 8,
        FORCE = 16,
        NOTMOVE = 32
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
        HURT_3_ANIM
    }
}
