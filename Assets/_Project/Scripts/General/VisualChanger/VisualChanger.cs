using UnityEngine;

namespace MadnessCooking.General {
    public abstract class VisualChanger : MonoBehaviour {
        protected bool _isActive;

        protected void Start() {
            UpdateVisual();
        }

        [ContextMenu("ChangeState")]
        public void ChangeState() {
            _isActive = !_isActive;
            UpdateVisual();
        }

        public void ChangeState(bool isActive) {
            _isActive = isActive;
            UpdateVisual();
        }

        protected abstract void UpdateVisual();
    }
}