using System;
using UnityEngine;

public class ScreenModeSettingsPoint : IntSettingsPoint, IBindable<VideoSettingsData> {
    [SerializeField] private ScreenSizeSettingsPoint _windowSizeSettings;
    [SerializeField] private LocalizedText _text;
    [SerializeField] private string _textExtension;

    protected override void Awake() {
        base.Awake();
        if ((_settingable.Value as ScreenModeManager) == null)
            throw new NullReferenceException($"{_settingable} is not ScreenModeManager");
    }

    protected override void UpdateState() {
        base.UpdateState();
        ScreenMode nowScreenMode = (_settingable.Value as ScreenModeManager).GetNowScreenMode();
        _windowSizeSettings.ChangeState(nowScreenMode == ScreenMode.Windowed);

        string modeName = _textExtension + nowScreenMode.ToString();
        _text.SetText(modeName);
    }

    public void Bind(VideoSettingsData data, bool isFileEmpty) {
        _data = data.ScreenMode;
        UpdateState();
    }
}
