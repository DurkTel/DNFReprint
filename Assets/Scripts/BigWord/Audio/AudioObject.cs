using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioObject
{
    private static int guid;

    public string assetName { get; private set; }

    public AudioSource audioSource { get; private set; }

    public float volume { get { return audioSource.volume; } set { audioSource.volume = value; } }

    public bool pause { get; set; }

    public float delay { get; set; }

    public GameObject gameObject { get; private set; }  

    public Transform transform { get; private set; }

    public AudioMixerGroup mixerGroup { get { return audioSource.outputAudioMixerGroup; } set { audioSource.outputAudioMixerGroup = value; } }

    public void Init(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.transform = gameObject.transform;
        this.audioSource = this.gameObject.TryAddComponent<AudioSource>();
    }

    private void LoadAudioClip(string assetName)
    {
        AssetLoader loader = AssetUtility.LoadAssetAsync<AudioClip>(assetName);
        loader.onComplete = (p) =>
        {
            PlayInternal(p.rawObject as AudioClip);
        };
    }

    private void PlayInternal(AudioClip clip)
    {
        audioSource.clip = clip;
        if (delay > 0f)
            audioSource.PlayDelayed(delay);
        else
            audioSource.Play();
    }

    public void Play(AudioClip clip, float volume = 1.0f, float delay = 0.0f)
    {
        this.volume = volume;
        this.delay = delay;
        this.gameObject.name = string.Format("[{0}]¡ª[{1}]", clip.name, ++guid);
        this.assetName = assetName;
        PlayInternal(clip);
    }

    public void Play(string assetName, float volume = 1.0f, float delay = 0.0f)
    {
        this.volume = volume;
        this.delay = delay;
        this.gameObject.name = string.Format("[{0}]¡ª[{1}]", assetName, ++guid);
        this.assetName = assetName;
        LoadAudioClip(assetName);
    }

    public void Release()
    {
        audioSource.clip = null;
        audioSource.loop = false;
        audioSource.outputAudioMixerGroup = null;
        pause = false;
        delay = 0f;
    }
}
