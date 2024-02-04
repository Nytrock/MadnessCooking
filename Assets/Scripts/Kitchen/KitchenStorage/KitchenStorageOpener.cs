using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KitchenStorageOpener : MonoBehaviour
{
    [SerializeField] private KitchenStorageUI _kitchenStorage;
    private Animator _animator;
    private bool _hasAnimation;

    private void Awake()
    {
        _hasAnimation = TryGetComponent(out _animator);
    }

    private void OnMouseDown()
    {
        _kitchenStorage.ChangePanelState();
        if (_hasAnimation)
            _animator.SetBool("isOpen", !_animator.GetBool("isOpen"));
    }
}
