using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private List<AudioObj> audioObjs;
    [Space]
    [SerializeField] private AudioSource audioSource;

    private Dictionary<AudioName, List<AudioClip>> audioClipDic = new Dictionary<AudioName, List<AudioClip>>();

    public bool Mute { get; set; }

    public override void Awake()
    {
        base.Awake();
        InitAudioClipDic();
        Mute = false;
    }

    private void InitAudioClipDic()
    {
        foreach (var audioObj in audioObjs)
        {
            if (!audioClipDic.ContainsKey(audioObj.audioName))
                audioClipDic.Add(audioObj.audioName, new List<AudioClip>());
            audioClipDic[audioObj.audioName].Add(audioObj.clip);
        }
    }

    public void PlayAudio(AudioName name)
    {
        if (audioSource != null && audioClipDic.ContainsKey(name))
        {
            var clips = audioClipDic[name];
            audioSource.PlayOneShot(clips[Random.Range(0, clips.Count)]);
        }
    }
}

[Serializable]
public class AudioObj
{
    public AudioName audioName;
    public AudioClip clip;
}
public enum AudioName
{
    Winnig, Matched, ModeSwitch, Bubble
}