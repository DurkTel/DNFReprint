using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputButton
{
    /// <summary>
    /// 正在按下中
    /// </summary>
    public bool IsPressing = false;
    /// <summary>
    /// 按下一瞬间的信号
    /// </summary>
    public bool OnPressed = false;
    /// <summary>
    /// 松开一瞬间的信号
    /// </summary>
    public bool OnReleased = false;
    /// <summary>
    /// 是否按键等待延申时间（双击）
    /// </summary>
    public bool IsExtending = false;
    /// <summary>
    /// 是否按键等待延迟输入时间
    /// </summary>
    public bool IsDelaying = false;

    public float extendingDuration = 0.3f;
    public float delayingDuration = 0.3f;

    protected bool curState = false;
    protected bool lastState = false;

    protected ButtonTimer exitTimer = new ButtonTimer();
    protected ButtonTimer delayTimer = new ButtonTimer();

    public void Tick(bool input)
    {
        exitTimer.Tick();
        delayTimer.Tick();

        IsPressing = curState = input;
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
    }


    protected void StartTimer(ButtonTimer timer,float duration)
    {
        timer.duration = duration;
        timer.InitTimer();
    }
}


public class ButtonTimer
{
    public enum STATE
    { 
        IDLE,
        RUN,
        FINISHED
    }
    public STATE state;

    public float duration = 1.0f;

    private float elapsedTime = 0;

    public void Tick()
    {
        switch (state)
        {
            case STATE.IDLE:
                break;
            case STATE.RUN:
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= duration)
                    state = STATE.FINISHED;
                break;
            case STATE.FINISHED:
                break;
            default:
                break;
        }
    }

    public void InitTimer()
    {
        elapsedTime = 0;
        state = STATE.RUN;
    }
}