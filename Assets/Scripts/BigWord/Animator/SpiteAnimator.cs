using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class SpiteAnimator : MonoBehaviour
{
    private float m_totalTime = 999;

    private int m_currentFrame;

    private SpriteRenderer m_spriteRenderer;
    public SpriteRenderer spriteRenderer { get { return m_spriteRenderer; } }   

    private bool m_running = false;

    public bool isLoop = false;

    public bool playOnAwake = true;

    public float interval = 0.2f;

    public float speed = 1f;

    public List<Sprite> sprites;

    public UnityAction onFinish;

    public void Awake()
    {
        if (m_spriteRenderer == null)
        {
            GameObject go = new GameObject("sprite");
            go.transform.SetParent(transform, false);
            m_spriteRenderer = go.AddComponent<SpriteRenderer>();
        }

        if (playOnAwake)
            Play();
    }

    private void Update()
    {
        if (!m_running) return;
        
        TickSpriteAnimation(Time.deltaTime);
    }

    private void OnEnable()
    {
        if (playOnAwake)
            Play();
    }

    public void Play()
    {
        m_running = true;
        m_currentFrame = 0;
        m_spriteRenderer.enabled = true;
    }

    public void TickSpriteAnimation(float deltaTime)
    {
        if (sprites.Count <= 0) return;

        m_totalTime += deltaTime;
        if (m_totalTime >= interval / speed)
        {
            m_totalTime = 0;

            m_spriteRenderer.sprite = sprites[m_currentFrame];  

            m_currentFrame++;

            if (m_currentFrame >= sprites.Count)
            {
                if (isLoop)
                { 
                    m_currentFrame = 0;
                    m_spriteRenderer.enabled = true;
                    m_running = true;
                }
                else
                {
                    m_spriteRenderer.enabled = false;
                    m_running = false;
                    onFinish?.Invoke();
                }
            }
        }
    }


}
