public class VerticalLocationSlider : LocationSlider {
    protected override void ChangeSliderValue() {
        _slider.value = _cameraManager.CameraTransform.position.y;
    }
}
