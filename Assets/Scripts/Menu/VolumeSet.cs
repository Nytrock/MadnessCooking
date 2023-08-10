using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSet : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource Press;
    public float musicSounds;
    public float effectSounds;
    public float UISounds;
    [SerializeField] private Slider SliderMusic;
    [SerializeField] private Slider SliderEffect;
    [SerializeField] private Slider sliderUI;

    public void SetVolume()
    {
        SliderMusic.value = musicSounds;
        mixer.SetFloat("Music", Mathf.Lerp(-30, 20, SliderMusic.value));
        SliderEffect.value = effectSounds;
        mixer.SetFloat("Effects", Mathf.Lerp(-30, 20, SliderEffect.value));
        sliderUI.value = UISounds;
        mixer.SetFloat("UI", Mathf.Lerp(-30, 20, sliderUI.value));
    }

    public void ChangeMusic(float volume)
    {
        Press.Play();
        mixer.SetFloat("Music", Mathf.Lerp(-30, 20, volume));
        musicSounds = volume;
    }

    public void ChangeEffect(float volume)
    {
        Press.Play();
        mixer.SetFloat("Effects", Mathf.Lerp(-30, 20, volume));
        effectSounds = volume;
    }
    public void ChangeUI(float volume)
    {
        Press.Play();
        mixer.SetFloat("UI", Mathf.Lerp(-30, 20, volume));
        UISounds = volume;
    }
}
