using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class KitchenStorageActivator : UIActivator
{
    [SerializeField] private KitchenStorageUI _kitchenStorage;
    private Animator _animator;
    private bool _hasAnimation;

    private void Awake()
    {
        _hasAnimation = TryGetComponent(out _animator);
    }

    protected override void Press()
    {
        _kitchenStorage.ChangePanelState();
        if (_hasAnimation)
            _animator.SetBool("isOpen", !_animator.GetBool("isOpen"));
    }
}
