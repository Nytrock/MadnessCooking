using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable]
    public class PitchableAudioInfo : AudioInfo {
        [SerializeField] private RangeFloat _pitch;

        public RangeFloat Pitch => _pitch;
    }
}
