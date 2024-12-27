using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UIActivatorAnimated : UIActivator {
    [SerializeField] private string _animationName = "isOpen";
    private Animator _animator;

    protected override void Awake() {
        base.Awake();
        _animator = GetComponent<Animator>();
    }

    protected override void Press() {
        base.Press();
        _animator.SetBool(_animationName, !_animator.GetBool(_animationName));
    }
}
