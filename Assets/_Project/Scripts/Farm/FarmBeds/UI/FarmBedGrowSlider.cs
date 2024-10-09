using UnityEngine;

public class FarmBedGrowSlider : GradientCircleSlider {
    [SerializeField] private VisualChanger _visual;

    public void ChangeState(bool newState) {
        _visual.ChangeState(newState);
    }
}
