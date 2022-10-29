using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.Audio;

public class GMAudioManager : MonoBehaviour
{
    public static GMAudioManager instance { get { return m_instance; }}

    private static GMAudioManager m_instance;

    private AudioMixer m_audioMixer;

    public Dictionary<string, AudioMixerGroup> audioMixerGroups = new Dictionary<string, AudioMixerGroup>(); 

    public Dictionary<string, AudioGroup> audioGroups = new Dictionary<string, AudioGroup>();

    public ObjectPool<AudioObject> audioObject;

    public static void Initialize()
    {
        GameObject go = new GameObject("GMAudioManager");
        go.AddComponent<AudioListener>();
        m_instance = go.AddComponent<GMAudioManager>();
        DontDestroyOnLoad(go);
    }

    public IEnumerator Init()
    {
        AssetLoader loader = AssetUtility.LoadAssetAsync<AudioMixer>("GameAudioMixer.mixer");

        yield return loader;

        m_audioMixer = loader.rawObject as AudioMixer;

        audioObject = new ObjectPool<AudioObject>((ao) => ao.Init(new GameObject()), (ao) => ao.Release());

        AudioMixerGroup[] audioMixerGroup = m_audioMixer.FindMatchingGroups("Master");
        foreach (var item in audioMixerGroup)
        {
            audioMixerGroups.Add(item.name, item);
        }
    }

    public static bool Play(string AudioGroupName, string assetName)
    {
        AudioGroup audioGroup;
        if (m_instance.audioGroups.TryGetValue(AudioGroupName, out audioGroup))
        {
            audioGroup.Play(assetName);
            return true;
        }

        Debug.LogError("没有AudioGroupName = " + AudioGroupName + "assetName = " + assetName + "的音效");
        return false;
    }

    public static void SetTotalAudio(float volume)
    {
        if (m_instance.audioMixerGroups.TryGetValue("Master", out AudioMixerGroup group))
        {
            group.audioMixer.SetFloat("Total", volume);
        }
    }

    public static void SetBgAudio(float volume)
    {
        if (m_instance.audioMixerGroups.TryGetValue("BGAudio", out AudioMixerGroup group))
        {
            group.audioMixer.SetFloat("BG", volume);
        }
    }

    public static void SetEffectAudio(float volume)
    {
        if (m_instance.audioMixerGroups.TryGetValue("EffectAudio", out AudioMixerGroup group))
        {
            group.audioMixer.SetFloat("Effect", volume);
        }
    }

    public static void SetUIAudio(float volume)
    {
        if (m_instance.audioMixerGroups.TryGetValue("UiAudio", out AudioMixerGroup group))
        {
            group.audioMixer.SetFloat("UI", volume);
        }
    }
}
