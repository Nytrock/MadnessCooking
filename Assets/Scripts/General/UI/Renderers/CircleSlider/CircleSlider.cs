using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CircleSlider : MonoBehaviour {
    private Image _slider;
    private float _maxValue = 1;
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

    private void UpdateValue() {
        float coef = Mathf.InverseLerp(0, _maxValue, _value);
        _slider.fillAmount = coef;
    }
}
