using UnityEngine;

public class VideoSettings : SettingsPanel, IBindable<VideoSettingsData> {
    [SerializeField] private ScreenModeSettingsPoint _screenMode;
    [SerializeField] private ScreenSizeSettingsPoint _screenSize;

    public void Bind(VideoSettingsData data, bool isFileEmpty) {
        _screenMode.Bind(data, isFileEmpty);
        _screenSize.Bind(data, isFileEmpty);
    }

    protected override void GenerateSettingPointsArray() {
        _settingPoints = new BaseSettingsPoint[] { _screenMode, _screenSize };
    }
}
