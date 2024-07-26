using System;

[Serializable]
public class AudioVolumeSettingsData {
    public SettingsPointData<float> MasterVolume;
    public SettingsPointData<float> UIVolume;
    public SettingsPointData<float> MusicVolume;
    public SettingsPointData<float> SfxVolume;

    public AudioVolumeSettingsData(float defaultValue) {
        MasterVolume = new(defaultValue);
        UIVolume = new(defaultValue);
        MusicVolume = new(defaultValue);
        SfxVolume = new(defaultValue);
    }
}
