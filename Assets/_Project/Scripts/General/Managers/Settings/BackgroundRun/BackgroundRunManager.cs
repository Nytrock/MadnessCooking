using UnityEngine;

public class BackgroundRunManager : MonoBehaviour, IBindable<GameSettingsData>, ISettingable<bool> {
    [SerializeField] private bool _defaultValue;

    private SettingsPointData<bool> _data;

    public bool DefaultValue {
        get {
            if (PlatformManager.IsNotDesktop)
                return false;
            return _defaultValue;
        }
    }

    public void Bind(GameSettingsData data) {
        data.BackgroundRunManager ??= new(DefaultValue);
        _data = data.BackgroundRunManager;
    }

    public void LateStart() {
        UpdateValue();
    }

    public void UpdateValue() {
        Application.runInBackground = _data.LastValue;
    }
}
