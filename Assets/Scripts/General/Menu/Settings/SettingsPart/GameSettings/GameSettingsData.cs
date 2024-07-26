using System;

[Serializable]
public class GameSettingsData : ISaveable {
    public SettingsPointData<int> LocalizationManager;
    public SettingsPointData<int> CursorManager;
    public SettingsPointData<bool> FpsManager;
}
