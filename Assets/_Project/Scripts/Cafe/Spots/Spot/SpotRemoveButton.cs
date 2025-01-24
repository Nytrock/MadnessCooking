using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ButtonWithAudio))]
public class SpotRemoveButton : MonoBehaviour, IPointerDownHandler {
    private ButtonWithAudio _button;
    private bool _canRemove;
    private CameraManager _cameraManager;

    public bool CanRemove => _canRemove;

    private void Awake() {
        _button = GetComponent<ButtonWithAudio>();
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

    public void Setup(UnityAction buttonAction, AudioSource buttonAudio) {
        _button.OverrideAllListeners(buttonAction);
        _button.SetAudio(buttonAudio);
    }
}
