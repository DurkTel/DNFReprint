using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OtherMotor : EntityMotor
{
    protected override void Start()
    {
        base.Start();
        m_spriceAnimator.DOSpriteAnimation(m_animationConfig.idle_Anim);

    }

    //public override void GetDamage(EntitySkill entitySkill)
    //{
        
    //}
}
