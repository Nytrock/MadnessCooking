public class FarmSlider : LocationSlider<FarmData> {
    protected override void ChangeSliderValue() {
        _slider.value = _cameraManager.MainCameraPos.position.y;
    }
}
