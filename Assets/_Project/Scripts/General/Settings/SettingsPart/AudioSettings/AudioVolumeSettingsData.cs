using System;

namespace MadnessCooking.General {
    [Serializable]
    public class AudioVolumeSettingsData {
        public SettingsPointData<float> MasterVolume { get; set; }
        public SettingsPointData<float> UIVolume { get; set; }
        public SettingsPointData<float> MusicVolume { get; set; }
        public SettingsPointData<float> SfxVolume { get; set; }

        public AudioVolumeSettingsData(float defaultValue) {
            MasterVolume = new(defaultValue);
            UIVolume = new(defaultValue);
            MusicVolume = new(defaultValue);
            SfxVolume = new(defaultValue);
        }
    }
}
