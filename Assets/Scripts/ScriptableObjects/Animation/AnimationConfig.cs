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

    public AnimationData airBorne_Anim;

    public AnimationData rebound_Anim;

    public AnimationData lie_Anim;

    public AnimationData jump_Anim;

    public AnimationData jumpRise_Anim;

    public AnimationData jumpDrop_Anim;

    public AnimationData jumpEnd_Anim;

    public AnimationData attack1_Anim;

    public AnimationData attack2_Anim;

    public AnimationData attack3_Anim;

    public AnimationData jumpAttack_Anim;

    public AnimationData runAttack_Anim;

    public AnimationData hit1_Anim;

    public AnimationData hit2_Anim;

    public AnimationData hit3_Anim;

    public List<AnimationData> CommonAnim = new List<AnimationData>();

    public List<AnimationData> JumpAnim = new List<AnimationData>();

    public List<AnimationData> AttackAnim = new List<AnimationData>();

    public List<AnimationData> HitAnim = new List<AnimationData>();

    public List<AnimationData> ForceAnim = new List<AnimationData>();

    public List<AnimationData> NotMoveAnim = new List<AnimationData>();

}

