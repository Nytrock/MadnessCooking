public class VideoSettingsLoader : LocalDataLoader<SettingsData, VideoSettingsData> {
    protected override void SetData(SettingsData data) {
        _data = data.VideoSettings;
    }
}
