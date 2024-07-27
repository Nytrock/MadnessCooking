using System;

[Serializable]
public class SettingsData : ISaveable {
    public GameSettingsData GameSettings;
    public AudioSettingsData AudioSettings;
    public VideoSettingsData VideoSettings;

    public SettingsData() {
        GameSettings = new();
        AudioSettings = new();
        VideoSettings = new();
    }
}
