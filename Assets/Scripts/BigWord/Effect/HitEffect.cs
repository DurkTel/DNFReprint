using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpiteAnimator))]
public class HitEffect : MonoBehaviour
{
    private SpiteAnimator m_animator;

    private RenenderSprite[] m_renenderSprites;

    public AnimationData animationData;

    public string assetName;

    public bool playAutomatic;
    public void OnEnable()
    {
        m_animator = GetComponent<SpiteAnimator>();
        m_renenderSprites = GetComponentsInChildren<RenenderSprite>();
        foreach (var item in m_renenderSprites)
        {
            item.InitSprite(assetName);
        }
        if (playAutomatic)
            Play();
        
    }

    public void Play(UnityAction action = null)
    {
        m_animator.DOSpriteAnimation(animationData);
        m_animator.animationFinish = action;
    }
}
