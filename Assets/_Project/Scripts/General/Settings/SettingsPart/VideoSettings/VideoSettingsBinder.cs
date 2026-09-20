namespace MadnessCooking.General {
    public class VideoSettingsBinder : LocalDataBinder<SettingsData, VideoSettingsData> {
        protected override void SetData(SettingsData data) {
            _data = data.VideoSettings;
        }
    }
}
