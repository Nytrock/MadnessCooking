using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class KitchenStorageUIActivator : UIActivator {
    private Animator _animator;
    private bool _hasAnimation;

    protected override void Awake() {
        base.Awake();
        _hasAnimation = TryGetComponent(out _animator);
    }

    protected override void Press() {
        base.Press();
        if (_hasAnimation)
            _animator.SetBool("isOpen", !_animator.GetBool("isOpen"));
    }
}
