using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    [RequireComponent(typeof(ButtonWithAudio))]
    public class SpotRemoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
        private ButtonWithAudio _button;
        private UnityAction _action;

        private bool _isPressed;
        private float _mouseOffset;

        private void Awake() {
            _button = GetComponent<ButtonWithAudio>();
        }

        private void Update() {
            if (_isPressed)
                _mouseOffset = Mathf.Max(Mathf.Abs(Input.GetAxis("Mouse X")), _mouseOffset);
        }

        public void OnPointerDown(PointerEventData eventData) {
            _isPressed = true;
            _mouseOffset = 0;
        }

        public void OnPointerUp(PointerEventData eventData) {
            if (_mouseOffset > 0.1f)
                return;

            _action.Invoke();
        }

        public void Setup(UnityAction buttonAction, AudioSource buttonAudio) {
            _action = buttonAction;
            _button.SetAudio(buttonAudio);
        }
    }
}
