public class CafeSlider : LocationSlider
{
    protected override void ChangeSliderValue() {
        _slider.value = _cameraManager.MainCameraPos.position.x;
    }
}
