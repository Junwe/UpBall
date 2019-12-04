using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SOUND
{
    S_JUMP = 0,
    S_DIE,
    S_BGM
}

public class Sound : MonoSingleton<Sound>
{
    public AudioSource[] effSource;
    public AudioSource bgmSource;
    public AudioClip[] audioClips;

    private Dictionary<SOUND, AudioClip> _soundDictionary = new Dictionary<SOUND, AudioClip>();

    private void Awake()
    {
        for (int i = 0; i < audioClips.Length; ++i)
        {
            _soundDictionary.Add((SOUND)i, audioClips[i]);
        }
        SetMute(PlayerPrefs.GetInt("SoundEff", 1),"SoundEff");
        SetMute(PlayerPrefs.GetInt("SoundBgm", 1),"SoundBgm");

        PlayBGMSound(SOUND.S_BGM);
    }

    private void Start()
    {
        PlayBGMSound(SOUND.S_BGM);

    }

    public void SetMute(int mute,string type)
    {
        bool isMute = mute == 0 ? true : false;
        if (type.Equals("SoundEff"))
        {
            for (int i = 0; i < effSource.Length; ++i)
            {
                effSource[i].mute = isMute;
            }
        }
        else if(type.Equals("SoundBgm"))
        {
            bgmSource.mute = isMute;
        }
        PlayerPrefs.SetInt(type, mute);
    }

    public void PlayEffSound(SOUND idx)
    {
        for (int i = 0; i < effSource.Length; ++i)
        {
            if (!effSource[i].isPlaying)
            {
                effSource[i].clip = _soundDictionary[idx];
                effSource[i].Play();
            }
        }
    }

    public void PlayBGMSound(SOUND idx)
    {
        bgmSource.clip = _soundDictionary[idx];
        bgmSource.loop = true;
        bgmSource.Play();
    }
}
