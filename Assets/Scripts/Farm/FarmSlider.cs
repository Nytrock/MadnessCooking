public class FarmSlider : LocationSlider
{
    protected override void ChangeSliderValue()
    {
        _slider.value = _cameraManager.MainCameraPos.position.y;
    }
}
