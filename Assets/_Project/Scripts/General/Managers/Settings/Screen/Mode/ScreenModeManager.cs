using System;
using UnityEngine;

public class ScreenModeManager : MonoBehaviour, IBindable<VideoSettingsData>, ISettingableWithOptions {
    [SerializeField] private ScreenMode[] _modes;
    [SerializeField] private ScreenMode _defaultMode;
    private VideoSettingsData _data;

    public int DefaultValue => Mathf.Max(Array.IndexOf(_modes, _defaultMode), 0);
    public int OptionsCount => _modes.Length;

    public void LateStart() { }

    public void Bind(VideoSettingsData data) {
        data.ScreenMode ??= new(DefaultValue);
        _data = data;
    }

    public ScreenMode GetNowScreenMode() {
        return _modes[_data.ScreenMode.LastValue];
    }

    public void UpdateValue() {
        ScreenMode nowScreenMode = GetNowScreenMode();
        Screen.fullScreen = nowScreenMode != ScreenMode.Windowed;

        FullScreenMode nowFullScreenMode;
        switch (nowScreenMode) {
            case ScreenMode.Windowed:
                nowFullScreenMode = FullScreenMode.Windowed;
                break;
            case ScreenMode.Fullscreen:
                nowFullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            default:
                nowFullScreenMode = FullScreenMode.FullScreenWindow;
                break;
        }

        Screen.fullScreenMode = nowFullScreenMode;
    }
}
