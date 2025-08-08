using UnityEngine;

public class ScreenSizeManager : MonoBehaviour, IBindable<VideoSettingsData>, ISettingableWithOptions {
    [SerializeField] private int[] _heights;
    [SerializeField] private ScreenModeManager _screenModeManager;

    private VideoSettingsData _data;
    private ScreenSize _nowScreenSize;
    private float _ratio;

    public ScreenSize NowScreenSize => _nowScreenSize;

    public int DefaultValue {
        get {
            for (int i = 0; i < _heights.Length; i++)
                if (_heights[i] >= Screen.height)
                    return i;
            return 0;
        }
    }

    public int OptionsCount => _heights.Length;

    public void LateStart() { }

    private void Awake() {
        _ratio = (float)Screen.width / Screen.height;
    }

    public void Bind(VideoSettingsData data) {
        data.ScreenSize ??= new(DefaultValue);
        _data = data;
    }

    public void UpdateValue() {
        int height = _heights[_data.ScreenSize.LastValue];
        int width = (int)(height * _ratio);

        _nowScreenSize = new(width, height);
        Screen.SetResolution(width, height, true);
        _screenModeManager.UpdateValue();
    }
}
