namespace MadnessCooking.General {
    public class AudioSettingsBinder : LocalDataBinder<SettingsData, AudioSettingsData> {
        protected override void SetData(SettingsData data) {
            _data = data.AudioSettings;
        }
    }
}
