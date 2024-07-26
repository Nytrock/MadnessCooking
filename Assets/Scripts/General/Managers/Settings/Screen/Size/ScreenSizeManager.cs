using System;
using UnityEngine;

public class ScreenSizeManager : MonoBehaviour, IBindable<VideoSettingsData>, ISettingableWithOptions {
    [SerializeField] private ScreenSize[] _sizes;
    [SerializeField] private ScreenSize _defaultSize;
    private VideoSettingsData _data;

    public int DefaultValue => Mathf.Max(Array.IndexOf(_sizes, _defaultSize), 0);
    public int OptionsCount => _sizes.Length;

    public void Bind(VideoSettingsData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.ScreenSize = new(DefaultValue);
        _data = data;
    }

    public ScreenSize GetNowScreenSize() {
        return _sizes[_data.ScreenSize.LastValue];
    }

    public void UpdateValue() {
        ScreenSize nowScreenSize = GetNowScreenSize();
        Screen.SetResolution(nowScreenSize.Width, nowScreenSize.Height, Screen.fullScreenMode);
    }
}
