public class GameSettingsLoader : LocalDataLoader<SettingsData, GameSettingsData> {
    protected override void SetData(SettingsData data) {
        _data = data.GameSettings;
    }
}
