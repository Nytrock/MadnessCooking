public class LocalizationSettingsPoint : IntSettingsPoint, IBindable<GameSettingsData> {
    public void Bind(GameSettingsData data, bool isFileEmpty) {
        _data = data.LocalizationManager;
    }
}
