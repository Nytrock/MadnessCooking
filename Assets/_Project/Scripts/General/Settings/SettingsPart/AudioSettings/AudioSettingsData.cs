using System;

namespace MadnessCooking.General {
    [Serializable]
    public class AudioSettingsData : ISaveable {
        public AudioVolumeSettingsData VolumeSettings { get; set; }
    }
}
