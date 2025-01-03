using System;

[Serializable]
public class GameSettingsData : ISaveable {
    public SettingsPointData<int> LocalizationManager { get; set; }
    public SettingsPointData<int> CursorManager { get; set; }
    public SettingsPointData<bool> FpsManager { get; set; }
    public SettingsPointData<bool> BackgroundRunManager { get; set; }
}
