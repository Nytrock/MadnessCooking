public class GameSettingsBinder : LocalDataBinder<SettingsData, GameSettingsData> {
    protected override void SetData(SettingsData data) {
        _data = data.GameSettings;
    }
}
