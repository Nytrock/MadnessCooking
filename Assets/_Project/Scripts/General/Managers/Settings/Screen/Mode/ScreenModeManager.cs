using System;
using UnityEngine;

namespace MadnessCooking.General {
    public class ScreenModeManager : MonoBehaviour, ISettingableWithOptions {
        [SerializeField] private ScreenMode _defaultMode;
        private VideoSettingsData _data;

        public int DefaultValue {
            get {
                if (PlatformManager.IsWeb)
                    return (int)ScreenMode.Windowed;
                return (int)_defaultMode;
            }
        }

        public int OptionsCount => Enum.GetNames(typeof(ScreenMode)).Length;

        public void SetSettings(SettingsData data) {
            data.VideoSettings.ScreenMode ??= new(DefaultValue);
            _data = data.VideoSettings;
        }

        public ScreenMode GetNowScreenMode() {
            return (ScreenMode)_data.ScreenMode.LastValue;
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
}
