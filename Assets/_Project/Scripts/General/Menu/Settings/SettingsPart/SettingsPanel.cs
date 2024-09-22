using System;
using UnityEngine;

public abstract class SettingsPanel : MonoBehaviour {
    [SerializeField] private GameObject _panel;
    protected BaseSettingsPoint[] _settingPoints;

    public event Action SettingsChanged;

    private void Awake() {
        ChangeState(false);

        GenerateSettingPointsArray();
        foreach (var point in _settingPoints)
            point.ValueChanged += InvokeSettingsChanged;
    }

    private void InvokeSettingsChanged() {
        SettingsChanged?.Invoke();
    }

    public void SetDefaultValues() {
        foreach (var settingPoint in _settingPoints)
            settingPoint.SetDefaultValue();
        InvokeSettingsChanged();
    }

    public void CancelChanges() {
        foreach (var settingPoint in _settingPoints)
            settingPoint.Cancel();
        InvokeSettingsChanged();
    }

    public void SubmitChanges() {
        foreach (var settingPoint in _settingPoints)
            settingPoint.Submit();
        InvokeSettingsChanged();
    }

    public void ChangeState(bool newState) {
        _panel.SetActive(newState);
    }

    public bool IsSettingsChanged() {
        bool result = false;
        foreach (var point in _settingPoints)
            result |= point.IsValueChanged;
        return result;
    }

    protected abstract void GenerateSettingPointsArray();
}
