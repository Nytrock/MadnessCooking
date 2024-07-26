public class AudioSettingsPoint : FloatSettingsPoint {
    public void Bind(SettingsPointData<float> data, bool isFileEmpty) {
        _data = data;
        UpdateState();
    }
}
