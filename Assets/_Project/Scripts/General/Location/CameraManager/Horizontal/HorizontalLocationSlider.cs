public class HorizontalLocationSlider : LocationSlider {
    protected override void ChangeSliderValue() {
        _slider.value = _cameraManager.CameraTransform.position.x;
    }
}
