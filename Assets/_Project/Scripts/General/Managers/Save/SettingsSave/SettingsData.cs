using System;

namespace MadnessCooking.General {
    [Serializable]
    public class SettingsData {
        public GameSettingsData GameSettings { get; set; }
        public AudioSettingsData AudioSettings { get; set; }
        public VideoSettingsData VideoSettings { get; set; }

        public SettingsData() {
            GameSettings = new();
            AudioSettings = new();
            VideoSettings = new();
        }
    }
}
