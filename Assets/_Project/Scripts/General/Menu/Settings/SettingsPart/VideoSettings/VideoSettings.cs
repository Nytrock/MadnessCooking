using UnityEngine;

public class VideoSettings : SettingsPanel, IBindable<VideoSettingsData> {
    [SerializeField] private ScreenModeSettingsPoint _screenMode;
    [SerializeField] private ScreenSizeSettingsPoint _screenSize;

    public void LateStart() {
        _screenMode.LateStart();
        _screenSize.LateStart();
    }

    public void Bind(VideoSettingsData data) {
        _screenMode.Bind(data);
        _screenSize.Bind(data);
    }

    protected override void GenerateSettingPointsArray() {
        _settingPoints = new BaseSettingsPoint[] { _screenMode, _screenSize };
    }
}
