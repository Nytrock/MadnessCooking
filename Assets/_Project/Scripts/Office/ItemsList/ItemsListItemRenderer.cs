using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemsListItemRenderer : MonoBehaviour {
    [SerializeField] private GameObject _selection;
    [SerializeField] private Image[] _selectionImages;

    private Button _button;
    private ItemsListItemDescription _description;
    private BuyableItem _item;

    private bool _isUnlocked;

    public BuyableItem Item => _item;
    public bool IsUnlocked => _isUnlocked;

    public void CheckNewItem(BuyableItem item) {
        if (item != _item)
            return;

        _isUnlocked = true;
        UpdateUnlockedState();
    }

    public void Setup(BuyableItem item, ItemsListItemDescription description) {
        _item = item;
        _description = description;

        SetupSelection();
        SetupButton();
        UpdateUnlockedState();
    }

    private void SetupButton() {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SelectItem);
        _button.image.sprite = _item.Icon;
    }

    private void SetupSelection() {
        foreach (var image in _selectionImages)
            image.sprite = _item.Icon;
        ChangeSelectionState(false);
    }

    private void UpdateUnlockedState() {
        if (_isUnlocked)
            _button.image.material = null;
        else
            _button.image.material = MaterialManager.Instance.BlackMaterial;
    }

    private void SelectItem() {
        _description.SelectItem(this);
    }

    public void ChangeSelectionState(bool isSelected) {
        _selection.SetActive(isSelected);
    }
}
