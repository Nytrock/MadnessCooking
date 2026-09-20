using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable]
    public class AudioInfo {
        [SerializeField] private AudioClip _audio;
        [SerializeField, Range(0, 1)] private float _volume = 0.5f;

        public AudioClip Audio => _audio;
        public float Volume => _volume;
    }
}
