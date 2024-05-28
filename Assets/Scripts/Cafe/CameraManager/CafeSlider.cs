public class CafeSlider : LocationSlider<CafeData> {
    protected override void ChangeSliderValue() {
        _slider.value = _cameraManager.MainCameraPos.position.x;
    }
}
