using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Animator))]
public abstract class ChoiceButton<TItem> : MonoBehaviour
    where TItem : BuyableItem {
    [SerializeField] protected Image _icon;

    protected Button _button;
    private bool _isChoosed;
    private Animator _animator;

    public TItem Item { get; protected set; }

    protected virtual void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void ChangeChoosedState() {
        _isChoosed = !_isChoosed;
        _animator.SetBool("isChoosed", _isChoosed);
    }

    public void ChangeState(bool newState) {
        gameObject.SetActive(newState);
    }

    public void Disable() {
        _button.onClick.RemoveAllListeners();
        ChangeState(false);
    }
}
