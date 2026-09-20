using UnityEngine;

namespace MadnessCooking.General {
    [RequireComponent(typeof(Animator))]
    public class UIActivatorAnimated : UIActivator {
        [SerializeField] private string _animationName = "isOpen";
        private Animator _animator;

        protected void Awake() {
            _animator = GetComponent<Animator>();
            _activableObject.Value.StateChanged += ChangeAnimationState;
        }

        private void ChangeAnimationState(bool newState) {
            _animator.SetBool(_animationName, newState);
        }
    }
}
