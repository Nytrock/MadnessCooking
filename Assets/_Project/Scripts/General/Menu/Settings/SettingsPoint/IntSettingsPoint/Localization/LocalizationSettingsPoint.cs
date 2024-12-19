public class LocalizationSettingsPoint : IntSettingsPoint, IBindable<GameSettingsData> {
    public void LateStart() { }

    public void Bind(GameSettingsData data) {
        _data = data.LocalizationManager;
    }
}
