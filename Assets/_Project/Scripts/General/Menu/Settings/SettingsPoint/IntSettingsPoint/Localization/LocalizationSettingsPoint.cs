public class LocalizationSettingsPoint : IntSettingsPoint, IBindable<GameSettingsData> {
    public void LateStart() {
        UpdateState();
    }

    public void Bind(GameSettingsData data) {
        _data = data.LocalizationManager;
    }
}
