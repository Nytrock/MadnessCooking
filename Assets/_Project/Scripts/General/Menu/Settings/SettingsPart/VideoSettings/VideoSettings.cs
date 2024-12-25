using UnityEngine;

public class VideoSettings : SettingsPanel, IBindable<VideoSettingsData> {
    [SerializeField] private ScreenSizeSettingsPoint _screenSize;
    [SerializeField] private ScreenModeSettingsPoint _screenMode;

    public void LateStart() {
        _screenSize.LateStart();
        _screenMode.LateStart();
    }

    public void Bind(VideoSettingsData data) {
        _screenSize.Bind(data);
        _screenMode.Bind(data);
    }

    protected override void GenerateSettingPointsArray() {
        _settingPoints = new BaseSettingsPoint[] { _screenMode, _screenSize };
    }
}
