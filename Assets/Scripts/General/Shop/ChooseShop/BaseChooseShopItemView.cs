using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class BaseChooseShopItemView<TItem> : MonoBehaviour
    where TItem : BuyableItem {

    [SerializeField] private ItemInfoRendererWithPrice _renderer;
    [SerializeField] private string _priceDescription;
    [SerializeField] private Button _buyButton;
    protected bool _isItemBuyable;
    protected TItem _selectedItem;

    protected virtual void Start() {
        ResetInfo();
    }

    public void SetButtonAction(UnityAction action) {
        _buyButton.onClick.AddListener(action);
    }

    public void ShowItem(TItem item, bool isBuyable) {
        _isItemBuyable = isBuyable;
        if (item == _selectedItem)
            ResetInfo();
        else
            SetInfo(item);
    }

    protected virtual void SetInfo(TItem item) {
        _selectedItem = item;
        _renderer.SetItemInfo(_selectedItem);
        _renderer.SetPrice(_priceDescription, _selectedItem.Price);
        UpdateButton();
    }

    public virtual void ResetInfo() {
        _selectedItem = null;
        _renderer.ResetInfo();
        UpdateButton();
    }

    private void UpdateButton() {
        _buyButton.interactable = _selectedItem != null
            && MoneyManager.Instance.MoneyCount >= _selectedItem.Price
            && _isItemBuyable;
    }

    public void UpdateBuyable(bool newValue) {
        _isItemBuyable = newValue;
        UpdateButton();
    }
}
