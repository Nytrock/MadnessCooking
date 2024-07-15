using UnityEngine;

public class GradientCircleSlider : CircleSlider {
    [SerializeField] private Gradient _gradient;

    protected override void UpdateValue() {
        base.UpdateValue();
        _slider.color = _gradient.Evaluate(_nowCoef);
    }
}
