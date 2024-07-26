using System;
using UnityEngine;

[Serializable]
public class SettingsPointData<TValue> {
    [SerializeField] private TValue _nowValue;
    [SerializeField] private TValue _lastValue;

    public TValue LastValue => _lastValue;
    public bool IsValueChanged => !Equals(_nowValue, _lastValue);

    public SettingsPointData(TValue defaultValue) {
        _nowValue = defaultValue;
        _lastValue = defaultValue;
    }

    public void ChangeValue(TValue newValue) {
        _lastValue = newValue;
    }

    public void CancelChanginng() {
        _lastValue = _nowValue;
    }

    public void SubmitChanginng() {
        _nowValue = _lastValue;
    }
}
