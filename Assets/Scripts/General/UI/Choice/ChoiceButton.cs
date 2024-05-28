using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ChoiceButton<TItem> : MonoBehaviour where TItem : BuyableObject {
    [SerializeField] private Sprite _deselectedSprite;
    [SerializeField] private Sprite _selectedSprite;
    [SerializeField] protected Image _icon;

    protected Button _button;
    private Image _image;
    private bool _isSelected;

    public TItem Item { get; protected set; }

    protected virtual void Awake() {
        _image = GetComponent<Image>();
        _image.sprite = _deselectedSprite;
    }

    public void ChangeSelectedState() {
        _isSelected = !_isSelected;
        if (_isSelected)
            _image.sprite = _selectedSprite;
        else
            _image.sprite = _deselectedSprite;
    }

    public void ChangeState(bool newState) {
        gameObject.SetActive(newState);
    }

    public void Disable() {
        _button.onClick.RemoveAllListeners();
        ChangeState(false);
    }
}
