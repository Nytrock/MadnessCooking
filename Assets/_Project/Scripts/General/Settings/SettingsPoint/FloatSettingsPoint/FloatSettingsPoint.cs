using UnityEngine;
using UnityEngine.UI;

namespace MadnessCooking.General {
    public class FloatSettingsPoint : SettingsPoint<float> {
        [SerializeField] private Slider _slider;

        protected override void UpdateState() {
            base.UpdateState();
            _slider.value = _data.LastValue;
        }

        private void Awake() {
            _slider.minValue = 0.001f;
            _slider.maxValue = 1;
            _slider.onValueChanged.AddListener(ChangeValue);
        }
    }
}
