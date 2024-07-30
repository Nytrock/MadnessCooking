public class LocalizationSettingsPoint : IntSettingsPoint, IBindable<GameSettingsData> {
    public void Bind(GameSettingsData data) {
        _data = data.LocalizationManager;
    }
}
