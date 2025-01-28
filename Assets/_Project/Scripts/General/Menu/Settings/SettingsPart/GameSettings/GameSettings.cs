using UnityEngine;

public class GameSettings : SettingsPanel, IBindable<GameSettingsData> {
    [SerializeField] private IntSettingsPoint _localization;
    [SerializeField] private CursorSettingsPoint _cursor;
    [SerializeField] private BoolSettingsPoint _fpsShow;
    [SerializeField] private BoolSettingsPoint _backgroundRun;

    public void Bind(GameSettingsData data) {
        _localization.Bind(data.LocalizationManager);
        _cursor.Bind(data.CursorManager);
        _fpsShow.Bind(data.FpsManager);
        _backgroundRun.Bind(data.BackgroundRunManager);
    }

    protected override void GenerateSettingPointsArray() {
        _settingPoints = new BaseSettingsPoint[] { _localization, _cursor, _fpsShow, _backgroundRun };
    }
}
