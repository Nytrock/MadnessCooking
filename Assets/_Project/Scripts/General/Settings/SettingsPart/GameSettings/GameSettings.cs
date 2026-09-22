using UnityEngine;

namespace MadnessCooking.General {
    public class GameSettings : SettingsPanel {
        [Header("Managers")]
        [SerializeField] private LocalizationManager _localizationManager;
        [SerializeField] private CursorManager _cursorManager;
        [SerializeField] private FpsManager _fpsManager;
        [SerializeField] private BackgroundRunManager _backgroundRunManager;

        [Header("Points")]
        [SerializeField] private IntSettingsPoint _localization;
        [SerializeField] private CursorSettingsPoint _cursor;
        [SerializeField] private BoolSettingsPoint _fpsShow;
        [SerializeField] private BoolSettingsPoint _backgroundRun;

        public void SetSettings(GameSettingsData data) {
            _localization.SetSettingable(_localizationManager);
            _cursor.SetSettingable(_cursorManager);
            _fpsShow.SetSettingable(_fpsManager);
            _backgroundRun.SetSettingable(_backgroundRunManager);

            _localization.SetData(data.LocalizationManager);
            _cursor.SetData(data.CursorManager);
            _fpsShow.SetData(data.FpsManager);
            _backgroundRun.SetData(data.BackgroundRunManager);
        }

        protected override void GenerateSettingPointsArray() {
            _settingPoints = new BaseSettingsPoint[] { _localization, _cursor, _fpsShow, _backgroundRun };
        }
    }
}
