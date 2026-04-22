using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private const string MuteKey = "AudioMuted";

    private void Awake()
    {
        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }

        bool isMuted = PlayerPrefs.GetInt(MuteKey, 0) == 1;
        SetMuteState(isMuted);
    }

    public void Play(SoundType soundType)
    {
        var s = Array.Find(sounds, sound => sound.type == soundType);
        s.source.Play();
    }
    
    public void Stop(SoundType soundType)
    {
        var s = Array.Find(sounds, sound => sound.type == soundType);
        s.source.Stop();
    }

    public void ToggleMute()
    {
        bool isMuted = PlayerPrefs.GetInt(MuteKey, 0) == 1;
        if (isMuted)
        {
            UnmuteAllSounds();
        }
        else
        {
            MuteAllSounds();
        }
    }

    private void SetMuteState(bool isMuted)
    {
        foreach (var sound in sounds)
        {
            sound.source.mute = isMuted;
        }
    }
    
    private void MuteAllSounds()
    {
        SetMuteState(true);
        PlayerPrefs.SetInt(MuteKey, 1); 
        PlayerPrefs.Save();
    }

    private void UnmuteAllSounds()
    {
        SetMuteState(false);
        PlayerPrefs.SetInt(MuteKey, 0); 
        PlayerPrefs.Save();
    }
}

public enum SoundType
{
    DefaultBoxBreak,
    GlassBoxBreak,
    GameOver,
    CannonShoot,
    AviaDeath,
    AviaFly
}