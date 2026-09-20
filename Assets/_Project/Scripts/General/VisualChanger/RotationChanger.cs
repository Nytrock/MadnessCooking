using UnityEngine;

namespace MadnessCooking.General {
    public class RotationChanger : VisualChanger {
        [SerializeField] private Vector3 _disabledRotation;
        [SerializeField] private Vector3 _activeRotation;

        protected override void UpdateVisual() {
            transform.rotation = Quaternion.Euler(_isActive ? _activeRotation : _disabledRotation);
        }
    }
}
