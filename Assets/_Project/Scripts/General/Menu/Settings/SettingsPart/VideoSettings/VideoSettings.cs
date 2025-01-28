using UnityEngine;

public class VideoSettings : SettingsPanel, IBindable<VideoSettingsData> {
    [SerializeField] private ScreenSizeSettingsPoint _screenSize;
    [SerializeField] private ScreenModeSettingsPoint _screenMode;

    public void Bind(VideoSettingsData data) {
        _screenSize.Bind(data.ScreenSize);
        _screenMode.Bind(data.ScreenMode);
    }

    protected override void GenerateSettingPointsArray() {
        _settingPoints = new BaseSettingsPoint[] { _screenMode, _screenSize };
    }
}
