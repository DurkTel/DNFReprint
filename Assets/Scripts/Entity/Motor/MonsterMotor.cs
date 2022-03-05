using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMotor : EntityMotor
{
    protected override void Start()
    {
        base.Start();
        m_spriceAnimator.DOSpriteAnimation(m_animationConfig.idle_Anim);

    }

    //public override void GetDamage(EntitySkill entitySkill)
    //{
    //    int rand = Random.Range(0, m_animationConfig.HitAnim.Count);
    //    m_spriceAnimator.DOSpriteAnimation(m_animationConfig.HitAnim[rand]);
    //}

    protected override void Update()
    {
        base.Update();
    }
}
