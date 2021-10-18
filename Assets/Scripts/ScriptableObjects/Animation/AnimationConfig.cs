using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Animation/AnimationConfig")]
public class AnimationConfig : ScriptableObject
{
    public AnimationData idle_Anim;

    public AnimationData idleTown_Anim;

    public AnimationData walk_Anim;

    public AnimationData run_Anim;

    public AnimationData sit_Anim;

    public AnimationData attack_Anim;

    public AnimationData attack2_Anim;

    public AnimationData attack3_Anim;

    public AnimationData jump_Anim;

    public AnimationData jumpRise_Anim;

    public AnimationData jumpDrop_Anim;

    public AnimationData jumpEnd_Anim;

    public AnimationData jump_Attack;
}
