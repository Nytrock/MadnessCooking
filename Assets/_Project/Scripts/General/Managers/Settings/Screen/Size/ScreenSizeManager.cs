using UnityEngine;

namespace MadnessCooking.General {
    public class ScreenSizeManager : MonoBehaviour, ISettingableWithOptions {
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

        private void Awake() {
            _ratio = (float)Screen.width / Screen.height;
        }

        public void SetSettings(SettingsData data) {
            data.VideoSettings.ScreenSize ??= new(DefaultValue);
            _data = data.VideoSettings;
        }

        public void UpdateValue() {
            int height, width;
            if (PlatformManager.IsWeb) {
                height = Screen.height;
                width = Screen.width;
            } else {
                height = _heights[_data.ScreenSize.LastValue];
                width = (int)(height * _ratio);
            }

            _nowScreenSize = new(width, height);
            Screen.SetResolution(width, height, Screen.fullScreenMode);
        }
    }
}
