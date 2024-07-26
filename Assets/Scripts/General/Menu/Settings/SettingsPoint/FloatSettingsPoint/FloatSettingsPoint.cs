using UnityEngine;
using UnityEngine.UI;

public abstract class FloatSettingsPoint : SettingsPoint<float> {
    [SerializeField] private Slider _slider;

    protected override void UpdateState() {
        base.UpdateState();
        _slider.value = _data.LastValue;
    }

    private void Awake() {
        _slider.onValueChanged.AddListener(ChangeValue);
        _slider.minValue = 0;
        _slider.maxValue = 1;
    }
}
