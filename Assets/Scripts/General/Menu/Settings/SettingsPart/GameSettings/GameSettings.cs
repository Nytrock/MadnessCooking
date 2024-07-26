using UnityEngine;

public class GameSettings : SettingsPanel, IBindable<GameSettingsData> {
    [SerializeField] private LocalizationSettingsPoint _localization;
    [SerializeField] private CursorSettingsPoint _cursor;
    [SerializeField] private FpsShowSettingsPoint _fpsShow;

    public void Bind(GameSettingsData data, bool isFileEmpty) {
        _localization.Bind(data, isFileEmpty);
        _cursor.Bind(data, isFileEmpty);
        _fpsShow.Bind(data, isFileEmpty);
    }

    protected override void GenerateSettingPointsArray() {
        _settingPoints = new BaseSettingsPoint[] { _localization, _cursor, _fpsShow };
    }
}
