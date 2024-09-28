using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class LocationAndOutputAudioInfo : LocationAudioInfo {
    [SerializeField] private AudioMixerGroup _mixerGroup;

    public AudioMixerGroup MixerGroup => _mixerGroup;
}
