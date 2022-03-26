using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerManager : SingletonMonoAuto<TimerManager>
{
    private Dictionary<int, Timer> m_allTimer = new Dictionary<int, Timer>();

    private Dictionary<int, Timer> m_waitAdd = new Dictionary<int, Timer>();

    private List<int> m_waitRemove = new List<int>();

    private Timer SetTimer(TimerType timerType, UnityAction callback, float delay, float interval, int duration)
    {
        Timer timer = new Timer().SetData(timerType, callback, delay, interval, duration);
        return timer;
    }

    public int AddTimer(UnityAction callback, float delay = 0f, float interval = 1f, int duration = 1)
    {
        Timer timer = SetTimer(TimerType.NOMAL, callback, delay, interval, duration);
        int id = timer.id;
        m_waitAdd.Add(id, timer);

        return id;
    }

    public int AddFrame(UnityAction callback, float delay = 0f, float interval = 1f, int duration = 1)
    {
        Timer timer = SetTimer(TimerType.FRAME, callback, delay, interval, duration);
        int id = timer.id;
        m_waitAdd.Add(id, timer);

        return id;
    }

    public void DelTimer(int id)
    {
        m_waitRemove.Add(id);
    }

    //启协协程
    public static Coroutine AddCoroutine(IEnumerator routine)
    {
        return Instance.StartCoroutine(routine);
    }

    //停止协程
    public static void DelCoroutine(Coroutine coroutine)
    {
        Instance.StopCoroutine(coroutine);
    }

    private void Update()
    {
        if (m_waitAdd.Count > 0)
        {
            foreach (var item in m_waitAdd)
            {
                m_allTimer.Add(item.Key, item.Value);
            }
            m_waitAdd.Clear();
        }

        if (m_waitRemove.Count > 0)
        {
            foreach (var key in m_waitRemove)
            {
                if (m_allTimer.ContainsKey(key))
                {
                    m_allTimer.Remove(key);
                }
            }
            m_waitRemove.Clear();
        }

        if (m_allTimer.Count > 0)
        {
            foreach (var item in m_allTimer)
            {
                item.Value.Update();

                if (item.Value.dispose)
                    m_waitRemove.Add(item.Key);
            }
        }

    }

    public enum TimerType
    { 
        FRAME,
        NOMAL
    }

    public class Timer
    {
        private static int m_ID = 99;

        public int id { get { return ++m_ID; } }

        private TimerType m_timerType;

        private float m_nextTime;

        private float m_nextFrame;

        private UnityAction m_callback;

        private float m_delay;

        private float m_interval;

        private int m_duration;

        private int m_allCount;

        private bool m_dispose;

        public bool dispose { get { return m_dispose; } }

        public Timer SetData(TimerType timerType, UnityAction callback, float delay, float interval, int duration)
        {
            this.m_timerType = timerType;
            this.m_callback = callback;
            this.m_delay = delay;
            this.m_interval = interval;
            this.m_duration = duration;
            this.m_nextFrame = Time.frameCount + this.m_delay + Mathf.Max(0, this.m_interval);
            this.m_nextTime = Time.realtimeSinceStartup + this.m_delay + Mathf.Max(0, this.m_interval);
            return this;
        }

        public void Clear()
        {
            m_dispose = true;
        }

        public void Update()
        {
            if (m_timerType == TimerType.FRAME)
            {
                if (Time.frameCount >= m_nextFrame)
                {
                    m_callback.Invoke();
                    m_allCount++;
                    m_nextFrame = Time.frameCount + Mathf.Max(0, m_interval);
                }
            }
            else
            {
                if (Time.realtimeSinceStartup >= m_nextTime)
                {
                    m_callback.Invoke();
                    m_allCount++;
                    m_nextTime = Time.realtimeSinceStartup + Mathf.Max(0, m_interval);
                }
            }

            if (m_duration > 0 && m_allCount >= m_duration)
                Clear();
        }
    }
}
