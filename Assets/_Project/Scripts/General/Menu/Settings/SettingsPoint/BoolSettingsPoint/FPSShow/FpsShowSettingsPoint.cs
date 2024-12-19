public class FpsShowSettingsPoint : BoolSettingsPoint, IBindable<GameSettingsData> {
    public void LateStart() {
        UpdateState();
    }

    public void Bind(GameSettingsData data) {
        _data = data.FpsManager;
    }
}
