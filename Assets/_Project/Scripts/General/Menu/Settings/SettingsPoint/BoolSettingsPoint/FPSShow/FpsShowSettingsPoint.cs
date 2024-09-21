public class FpsShowSettingsPoint : BoolSettingsPoint, IBindable<GameSettingsData> {
    public void Bind(GameSettingsData data) {
        _data = data.FpsManager;
        UpdateState();
    }
}
