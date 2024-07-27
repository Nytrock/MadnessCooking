public class AudioSettingsLoader : LocalDataLoader<SettingsData, AudioSettingsData> {
    protected override void SetData(SettingsData data) {
        _data = data.AudioSettings;
    }
}
