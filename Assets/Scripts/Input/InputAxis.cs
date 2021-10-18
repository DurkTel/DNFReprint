using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAxis : InputButton
{

    public float Axis = 0.0f;
    public void Tick(float axis)
    {
        exitTimer.Tick();
        delayTimer.Tick();
        
        IsPressing = curState = Mathf.Abs(axis) > 0;
        OnPressed = false;
        OnReleased = false;

        if (curState != lastState)
        {
            if (curState)
            {
                OnPressed = true;
                StartTimer(delayTimer, delayingDuration);
            }
            else
            {
                OnReleased = true;
                StartTimer(exitTimer, extendingDuration);
            }
        }

        lastState = curState;
        IsExtending = exitTimer.state == ButtonTimer.STATE.RUN;
        IsDelaying = delayTimer.state == ButtonTimer.STATE.RUN;
        Axis = axis;
    }
}
