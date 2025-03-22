using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class SettingsPointData<TValue> {
    [SerializeField, JsonProperty] private TValue _nowValue;
    [SerializeField, JsonProperty] private TValue _lastValue;

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

    public void SubmitChanging() {
        _nowValue = _lastValue;
    }
}
