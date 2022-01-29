using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OtherMotor : EntityMotor, IDamage
{
    protected override void Start()
    {
        base.Start();
        m_spriceAnimator.DOSpriteAnimation(m_animationConfig.CommonAnim.idle_Anim);

    }

    public void GetDamage(EntitySkill entitySkill)
    {
        m_spriceAnimator.DOSpriteAnimation(m_animationConfig.HitAnim.hit1_Anim);
    }
}
