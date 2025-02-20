using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UIActivatorAnimated : UIActivator {
    [SerializeField] private string _animationName = "isOpen";
    private Animator _animator;

    protected void Awake() {
        _animator = GetComponent<Animator>();
    }

    protected override void Press() {
        base.Press();
        _animator.SetBool(_animationName, !_animator.GetBool(_animationName));
    }
}
