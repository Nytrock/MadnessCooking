public class FpsShowSettingsPoint : BoolSettingsPoint, IBindable<GameSettingsData> {
    public void Bind(GameSettingsData data, bool isFileEmpty) {
        _data = data.FpsManager;
        UpdateState();
    }
}
