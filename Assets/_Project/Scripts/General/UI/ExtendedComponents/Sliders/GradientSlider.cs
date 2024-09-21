using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class GradientSlider : MonoBehaviour {
    [SerializeField] private Gradient _gradient;
    private Slider _slider;
    private Image _sliderImage;

    private void Awake() {
        _slider = GetComponent<Slider>();
        _sliderImage = _slider.fillRect.GetComponent<Image>();
        _slider.onValueChanged.AddListener(UpdateColor);
    }

    private void UpdateColor(float value) {
        _slider.value = value;
        _sliderImage.color = _gradient.Evaluate(value / _slider.maxValue);
    }

    public void SetGradient(Gradient gradient) {
        _gradient = gradient;
    }
}
