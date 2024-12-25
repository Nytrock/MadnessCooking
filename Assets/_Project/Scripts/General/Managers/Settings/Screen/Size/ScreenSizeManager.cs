using UnityEngine;

public class ScreenSizeManager : MonoBehaviour, IBindable<VideoSettingsData>, ISettingableWithOptions {
    [SerializeField] private ScreenSize[] _sizes;
    [SerializeField] private ScreenSize _defaultSize;
    [SerializeField] private ScreenModeManager _screenModeManager;
    private VideoSettingsData _data;

    public int DefaultValue {
        get {
            for (int i = 0; i < _sizes.Length; i++) {
                if (_sizes[i].Width == _defaultSize.Width) {
                    return i;
                }
            }

            return 0;
        }
    }

    public int OptionsCount => _sizes.Length;

    public void LateStart() { }

    public void Bind(VideoSettingsData data) {
        data.ScreenSize ??= new(DefaultValue);
        _data = data;
    }

    public ScreenSize GetNowScreenSize() {
        return _sizes[_data.ScreenSize.LastValue];
    }

    public void UpdateValue() {
        ScreenSize nowScreenSize = GetNowScreenSize();
        Screen.SetResolution(nowScreenSize.Width, nowScreenSize.Height, true);
        _screenModeManager.UpdateValue();
    }
}
