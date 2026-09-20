using System;
using UnityEngine;
using UnityEngine.Audio;

namespace MadnessCooking.General {
    [Serializable]
    public class LocationAndOutputAudioInfo : LocationAudioInfo {
        [SerializeField] private AudioMixerGroup _mixerGroup;

        public AudioMixerGroup MixerGroup => _mixerGroup;
    }
}
