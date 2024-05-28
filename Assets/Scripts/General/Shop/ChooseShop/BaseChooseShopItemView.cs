using UnityEngine;
using UnityEngine.UI;

public abstract class BaseChooseShopItemView : MonoBehaviour {
    [SerializeField] protected BaseChooseShop _shop;
    [SerializeField] private ItemInfoRendererWithPrice _renderer;
    [SerializeField] private string _priceDescription;
    [SerializeField] private Button _buyButton;
    protected BuyableObject _itemToBuy;

    private void Start() {
        ResetInfo();
        UpdateButton();
    }

    public void ShowItem(BuyableObject item) {
        if (item == _itemToBuy)
            ResetInfo();
        else
            SetInfo(item);
    }

    protected virtual void SetInfo(BuyableObject item) {
        _itemToBuy = item;
        _renderer.SetItemInfo(_itemToBuy);
        _renderer.SetPrice(_priceDescription, _itemToBuy.Price);
        UpdateButton();
    }

    public virtual void ResetInfo() {
        _itemToBuy = null;
        _renderer.ResetInfo();
        UpdateButton();
    }

    private void UpdateButton() {
        _buyButton.interactable = _itemToBuy != null
            && MoneyManager.Instance.MoneyCount >= _itemToBuy.Price;
    }

    public virtual void BuyChosen() {
        _shop.BuyItem(_itemToBuy);
        ResetInfo();
    }
}
