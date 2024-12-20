public class AudioSettingsPoint : FloatSettingsPoint {
    public void LateStart() {
        UpdateState();
    }

    public void Bind(SettingsPointData<float> data) {
        _data = data;
    }
}
