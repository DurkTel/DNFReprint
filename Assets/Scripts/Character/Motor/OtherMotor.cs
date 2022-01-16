using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherMotor : EntityMotor
{
    protected override void Start()
    {
        base.Start();
        m_spriceAnimator.DOSpriteAnimation(m_animationConfig.CommonAnim.idle_Anim);

    }

    private void GetDamage(EntitySkill entitySkill)
    {
        m_spriceAnimator.DOSpriteAnimation(m_animationConfig.HitAnim.hit1_Anim);
    }
}
