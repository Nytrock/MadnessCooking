using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ChoiceButton<T, K> : MonoBehaviour
{
    [SerializeField] private Sprite _deselectedSprite;
    [SerializeField] private Sprite _selectedSprite;
    [SerializeField] protected Image _icon;
    protected Button _button;
    private Image _image;
    [SerializeField] protected T _item;
    private bool _isSelected;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.sprite = _deselectedSprite;
    }

    public void ChangeSelectedState()
    {
        _isSelected = !_isSelected;
        if (_isSelected)
            _image.sprite = _selectedSprite;
        else
            _image.sprite = _deselectedSprite;
    }

    public virtual void Setup(T item, int index, K ui)
    {
        _button = GetComponent<Button>();
        gameObject.SetActive(true);
        _item = item;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
