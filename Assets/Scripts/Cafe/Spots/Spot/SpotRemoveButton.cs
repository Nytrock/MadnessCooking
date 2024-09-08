using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SpotRemoveButton : MonoBehaviour, IPointerDownHandler {
    private Button _button;
    private bool _canRemove;
    private CameraManager _cameraManager;

    public bool CanRemove => _canRemove;
    public Button Button => _button;

    private void Awake() {
        _button = GetComponent<Button>();
    }

    private void Update() {
        if (_canRemove)
            _canRemove &= !_cameraManager.IsMouseMoving;
    }

    public void OnPointerDown(PointerEventData eventData) {
        _canRemove = true;
    }

    public void SetCameraManager(CameraManager cameraManager) {
        _cameraManager = cameraManager;
    }
}
