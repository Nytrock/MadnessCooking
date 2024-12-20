using UnityEngine;

public class GameSettings : SettingsPanel, IBindable<GameSettingsData> {
    [SerializeField] private LocalizationSettingsPoint _localization;
    [SerializeField] private CursorSettingsPoint _cursor;
    [SerializeField] private FpsShowSettingsPoint _fpsShow;

    public void LateStart() {
        _localization.LateStart();
        _cursor.LateStart();
        _fpsShow.LateStart();
    }

    public void Bind(GameSettingsData data) {
        _localization.Bind(data);
        _cursor.Bind(data);
        _fpsShow.Bind(data);
    }

    protected override void GenerateSettingPointsArray() {
        _settingPoints = new BaseSettingsPoint[] { _localization, _cursor, _fpsShow };
    }
}
