using System;

namespace MadnessCooking.General {
    [Serializable]
    public class VideoSettingsData {
        public SettingsPointData<int> ScreenMode { get; set; }
        public SettingsPointData<int> ScreenSize { get; set; }
    }
}
