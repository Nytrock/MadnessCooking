public class AudioSettingsPoint : FloatSettingsPoint {
    public void Bind(SettingsPointData<float> data) {
        _data = data;
        UpdateState();
    }
}
