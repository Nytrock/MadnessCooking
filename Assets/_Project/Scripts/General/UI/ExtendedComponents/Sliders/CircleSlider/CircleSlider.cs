using UnityEngine;
using UnityEngine.UI;

namespace MadnessCooking.General {
    [RequireComponent(typeof(Image))]
    public class CircleSlider : MonoBehaviour {
        [SerializeField] private float _maxValue = 1;
        protected Image _slider;
        protected float _nowCoef;
        private float _value = 0;

        private void Awake() {
            _slider = GetComponent<Image>();
            _slider.type = Image.Type.Filled;
            UpdateValue();
        }

        public void SetValue(float value) {
            _value = value;
            UpdateValue();
        }

        public void SetMaxValue(float value) {
            _maxValue = value;
        }

        protected virtual void UpdateValue() {
            _nowCoef = Mathf.InverseLerp(0, _maxValue, _value);
            _slider.fillAmount = _nowCoef;
        }
    }
}
