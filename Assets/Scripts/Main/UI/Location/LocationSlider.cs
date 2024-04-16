using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class LocationSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] protected CameraManager _cameraManager;
    protected Slider _slider;

    private float _startPosition;
    private float _endPosition;

    private bool _isDragging;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _cameraManager.BordersFound += SetSliderValues;
        _cameraManager.CameraMoved += ChangeSliderValue;
    }

    private void SetSliderValues() {
        _startPosition = _cameraManager.StartPosition;
        _endPosition = _cameraManager.EndPosition;
        _slider.minValue = _startPosition;
        _slider.maxValue = _endPosition;
    }

    public void ChangeCameraPosition(float newPosition)
    {
        if (_isDragging)
            _cameraManager.SetCameraPosition(newPosition);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;
        _cameraManager.ChangeWorkMode(!_isDragging);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;
        _cameraManager.ChangeWorkMode(!_isDragging);
    }

    protected abstract void ChangeSliderValue();
}
