using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactMotor : BaseMotor
{

    protected override void InitAnimEvent()
    {
        base.InitAnimEvent();
        m_spriceAnimator.EVENT_AirAttackCombo += () =>
        {
            airAttackCombo++;
        };
    }

    protected override void Update()
    {
        base.Update();
        //SkillUpdate();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

}
