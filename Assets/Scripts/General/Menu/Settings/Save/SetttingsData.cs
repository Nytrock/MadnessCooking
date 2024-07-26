using System;

[Serializable]
public class SetttingsData : ISaveable {
    public GameSettingsData GameSettings;
    public AudioSettingsData AudioSettings;
    public VideoSettingsData VideoSettings;

    public SetttingsData() {
        GameSettings = new();
        AudioSettings = new();
        VideoSettings = new();
    }
}
