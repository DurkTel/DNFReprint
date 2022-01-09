using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Animation/AnimationConfig")]
public class AnimationConfig : ScriptableObject
{
    public CommonAnim commonAnim;

    public JumpAnim JumpAnim;

    public AttackAnim attackAnim;
}

public interface IBaseAnim { }

[System.Serializable]
public struct CommonAnim : IBaseAnim
{
    public AnimationData idle_Anim;

    public AnimationData idleTown_Anim;

    public AnimationData walk_Anim;

    public AnimationData run_Anim;

    public AnimationData sit_Anim;
}

[System.Serializable]
public struct JumpAnim : IBaseAnim
{
    public AnimationData jump_Anim;

    public AnimationData jumpRise_Anim;

    public AnimationData jumpDrop_Anim;

    public AnimationData jumpEnd_Anim;
}

[System.Serializable]
public struct AttackAnim : IBaseAnim
{
    public AnimationData attack1_Anim;

    public AnimationData attack2_Anim;

    public AnimationData attack3_Anim;

    public AnimationData jumpAttack_Anim;

    public AnimationData runAttack_Anim;

}
