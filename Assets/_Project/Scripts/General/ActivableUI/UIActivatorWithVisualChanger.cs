using UnityEngine;

namespace MadnessCooking.General {
    public class UIActivatorWithVisualChanger : UIActivator {
        [SerializeField] private VisualChanger _changer;

        private void Awake() {
            _activableObject.StateChanged += _changer.ChangeState;
        }
    }
}
