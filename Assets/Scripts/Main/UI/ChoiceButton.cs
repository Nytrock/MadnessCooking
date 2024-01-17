using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    [SerializeField] private Sprite _deselectedSprite;
    [SerializeField] private Sprite _selectedSprite;
    [SerializeField] protected Image _icon;
    private Image _image;
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
}
