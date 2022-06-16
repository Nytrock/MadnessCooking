using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSet : MonoBehaviour
{
    public AudioMixerGroup Mixer;
    public AudioSource Press;
    public float AllSounds;
    public float MusicSounds;
    public float EffectSounds;
    public Slider SliderAll;
    public Slider SliderMusic;
    public Slider SliderEffect;

    public void SetVolume()
    {
        SliderAll.value = AllSounds;
        Mixer.audioMixer.SetFloat("Master", Mathf.Lerp(-30, 20, SliderAll.value));
        SliderMusic.value = MusicSounds;
        Mixer.audioMixer.SetFloat("Music", Mathf.Lerp(-30, 20, SliderMusic.value));
        SliderEffect.value = EffectSounds;
        Mixer.audioMixer.SetFloat("Effects", Mathf.Lerp(-30, 20, SliderEffect.value));
    }

    public void ChangeAllVolume(float volume)
    {
        Press.Play();
        Mixer.audioMixer.SetFloat("Master", Mathf.Lerp(-30, 20, volume));
        AllSounds = volume;
    }

    public void ChangeMusic(float volume)
    {
        Mixer.audioMixer.SetFloat("Music", Mathf.Lerp(-30, 20, volume));
        MusicSounds = volume;
    }

    public void ChangeEffect(float volume)
    {
        Press.Play();
        Mixer.audioMixer.SetFloat("Effects", Mathf.Lerp(-30, 20, volume));
        EffectSounds = volume;
    }
}
