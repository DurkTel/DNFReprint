using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioGroup : MonoBehaviour
{
    public enum AuidioPlayMode
    {
        /// <summary>
        /// 是否允许多个声音同时播放 true的话切断当前 播放下一个
        /// </summary>
        Single,
        /// <summary>
        /// 是否只允许同时播一个 true的话 如果当前有在播 不会播另外的
        /// </summary>
        Only,
        /// <summary>
        /// 是否允许多个声音同时播放 true的话可以多个音效同时播放
        /// </summary>
        Multiple,
    }

    public AuidioPlayMode playMode = AuidioPlayMode.Multiple;

    public AudioMixerGroup audioMixerGroup;

    public List<AudioObject> activeAudios = new List<AudioObject>();

    public List<AudioObject> unActiveAudios = new List<AudioObject>();

    public bool isLoop;

    private AudioObject Create()
    {
        AudioObject ao = GMAudioManager.instance.audioObject.Get();
        ao.transform.SetParent(transform);
        ao.mixerGroup = audioMixerGroup;
        ao.audioSource.loop = isLoop;
        return ao;
    }

    private AudioObject GetAudioObjectFromActive()
    {
        AudioObject ao = null;

        if (activeAudios.Count > 0)
        {
            ao = activeAudios[0];
            ao.Release();
        }
        else if (unActiveAudios.Count > 0)
        {
            ao = unActiveAudios[0];
            ao.Release();
            unActiveAudios.RemoveAt(0);
            activeAudios.Add(ao);
        }
        else
        {
            ao = Create();
            activeAudios.Add(ao);
        }
        return ao;
    }

    private AudioObject GetAudioObjectFromUnActive()
    {
        AudioObject ao = null;

        if (unActiveAudios.Count > 0)
        {
            ao = unActiveAudios[0];
            ao.Release();
            unActiveAudios.RemoveAt(0);
            activeAudios.Add(ao);
        }
        else
        {
            ao = Create();
            activeAudios.Add(ao);
        }
        return ao;
    }

    private AudioObject GetAudioObject()
    {
        AudioObject ao = null;

        if (playMode == AuidioPlayMode.Multiple || (playMode == AuidioPlayMode.Only && activeAudios.Count <= 0))
        {
            ao = GetAudioObjectFromUnActive();
        }
        else if (playMode == AuidioPlayMode.Single)
        {
            ao = GetAudioObjectFromActive();
        }


        return ao;
    }

    public void Play(string assetName)
    {
        AudioObject ao = GetAudioObject();
        if (ao == null) return;
        ao.Play(assetName);
    }


    private void Update()
    {
        if (Time.frameCount % 60 == 0)
        {
            for (int i = 0; i < activeAudios.Count; i++)
            {
                if (!activeAudios[i].audioSource.isPlaying)
                {
                    unActiveAudios.Add(activeAudios[i]);
                    activeAudios.RemoveAt(i);
                }
            }
        }

    }
}
