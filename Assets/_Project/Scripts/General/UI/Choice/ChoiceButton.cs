using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonWithAudio), typeof(Animator))]
public abstract class ChoiceButton<TItem> : MonoBehaviour where TItem : BuyableItem {
    [SerializeField] protected Image _icon;

    protected Button _button;
    protected bool _isChoosed;
    private Animator _animator;

    public TItem Item { get; protected set; }

    protected virtual void Awake() {
        _animator = GetComponent<Animator>();
    }

    public virtual void Setup(TItem item, UnityAction buttonEvent) {
        CheckButton();
        _button.onClick.AddListener(buttonEvent);
        gameObject.SetActive(true);
        Item = item;
    }

    private void CheckButton() {
        if (_button != null) return;

        _button = GetComponent<Button>();
    }

    public virtual void ChangeChoosedState() {
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
