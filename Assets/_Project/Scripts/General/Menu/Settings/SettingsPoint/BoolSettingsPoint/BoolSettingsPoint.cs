using UnityEngine;
using UnityEngine.UI;

public class BoolSettingsPoint : SettingsPoint<bool> {
    [SerializeField] private Toggle _toggle;

    protected override void UpdateState() {
        base.UpdateState();
        _toggle.isOn = _data.LastValue;
    }

    private void Awake() {
        _toggle.onValueChanged.AddListener(ChangeValue);
    }
}
