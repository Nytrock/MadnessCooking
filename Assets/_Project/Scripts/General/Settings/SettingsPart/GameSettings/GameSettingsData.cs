using System;

namespace MadnessCooking.General {
    [Serializable]
    public class GameSettingsData {
        public SettingsPointData<int> LocalizationManager { get; set; }
        public SettingsPointData<int> CursorManager { get; set; }
        public SettingsPointData<bool> FpsManager { get; set; }
        public SettingsPointData<bool> BackgroundRunManager { get; set; }
    }
}
