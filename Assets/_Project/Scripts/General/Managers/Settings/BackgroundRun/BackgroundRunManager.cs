using UnityEngine;

namespace MadnessCooking.General {
    public class BackgroundRunManager : MonoBehaviour, ISettingable<bool> {
        [SerializeField] private bool _defaultValue;

        private SettingsPointData<bool> _data;

        public bool DefaultValue {
            get {
                if (PlatformManager.IsNotDesktop)
                    return false;
                return _defaultValue;
            }
        }

        public void SetSettings(SettingsData data) {
            data.GameSettings.BackgroundRunManager ??= new(DefaultValue);
            _data = data.GameSettings.BackgroundRunManager;
            UpdateValue();
        }

        public void UpdateValue() {
            Application.runInBackground = _data.LastValue;
        }
    }
}
