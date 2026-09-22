using UnityEngine;

namespace MadnessCooking.General {
    public class VideoSettings : SettingsPanel {
        [Header("Managers")]
        [SerializeField] private ScreenSizeManager _screenSizeManager;
        [SerializeField] private ScreenModeManager _screenModeManager;

        [Header("Points")]
        [SerializeField] private ScreenSizeSettingsPoint _screenSize;
        [SerializeField] private ScreenModeSettingsPoint _screenMode;

        public void SetSettings(VideoSettingsData data) {
            _screenMode.SetSettingable(_screenModeManager);
            _screenSize.SetSettingable(_screenSizeManager);

            _screenSize.SetData(data.ScreenSize);
            _screenMode.SetData(data.ScreenMode);
        }

        protected override void GenerateSettingPointsArray() {
            _settingPoints = new BaseSettingsPoint[] { _screenMode, _screenSize };
        }
    }
}
