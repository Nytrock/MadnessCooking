using System;

[Serializable]
public class VideoSettingsData : ISaveable {
    public SettingsPointData<int> ScreenMode;
    public SettingsPointData<int> ScreenSize;
}
