using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class BaseChooseShopItemView<TItem> : MonoBehaviour
    where TItem : BuyableItem {

    [SerializeField] private ItemInfoRendererWithPrice _renderer;
    [SerializeField] private string _priceDescription;
    [SerializeField] private Button _buyButton;
    [SerializeField] private MoneyManager _moneyManager;
    protected bool _isItemBuyable;
    protected TItem _selectedItem;

    private void Awake() {
        _moneyManager.MoneyChanged += delegate { UpdateButton(); };
    }

    protected virtual void Start() {
        ResetInfo();
    }

    public void SetButtonAction(UnityAction action) {
        _buyButton.onClick.AddListener(action);
    }

    public void UpdateItem(TItem item, bool isBuyable) {
        _isItemBuyable = isBuyable;
        _selectedItem = item;

        if (_selectedItem == null)
            ResetInfo();
        else
            SetInfo();
    }

    protected virtual void SetInfo() {
        _renderer.SetItemInfo(_selectedItem);
        _renderer.SetPrice(_priceDescription, _selectedItem.Price);
        UpdateButton();
    }

    public virtual void ResetInfo() {
        _renderer.ResetInfo();
        UpdateButton();
    }

    private void UpdateButton() {
        _buyButton.interactable = _selectedItem != null
            && _moneyManager.MoneyCount >= _selectedItem.Price
            && _isItemBuyable;
    }

    public void UpdateBuyable(bool newValue) {
        _isItemBuyable = newValue;
        UpdateButton();
    }
}
