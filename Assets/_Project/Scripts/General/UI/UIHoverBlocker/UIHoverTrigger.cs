using UnityEngine;
using UnityEngine.EventSystems;

namespace MadnessCooking.General {
    [RequireComponent(typeof(RectTransform))]
    public class UIHoverTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [SerializeField] private UIHoverListener _hoverListener;
        [SerializeField] private bool _isScrollBlocked;
        [SerializeField] private bool _isDebug;

        private RectTransform _rect;

        private void Awake() {
            _rect = GetComponent<RectTransform>();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            Enter();
        }

        public void OnPointerExit(PointerEventData eventData) {
            Leave();
        }

        public void OnDisable() {
            Leave();
        }

        public void OnEnable() {
            if (_rect.ContainsMouse())
                Enter();
        }

        private void Enter() {
            ChangeScrollState(_isScrollBlocked);
        }

        private void Leave() {
            ChangeScrollState(false);
        }

        private void ChangeScrollState(bool isScrollBlocked) {
            _hoverListener.ChangeScrollBlockState(isScrollBlocked);

            if (_isDebug) {
                Debug.Log($"{name} change scroll block to {isScrollBlocked}");
            }
        }
    }
}
