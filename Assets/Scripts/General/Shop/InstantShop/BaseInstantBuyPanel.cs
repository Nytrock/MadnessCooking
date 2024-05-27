using UnityEngine;

public abstract class BaseInstantBuyPanel : BaseBuyPanel
{
    [SerializeField] private ItemInfoRendererWithPrice _itemInfoRenderer;
    [SerializeField] protected string _priceDescription;

    public override void Setup(BuyableObject item, BaseShop shop)
    {
        base.Setup(item, shop);
        MoneyManager.Instance.MoneyChanged += UpdateButton;
    }

    public override void SetVisual(BuyableObject item)
    {
        _itemInfoRenderer.SetItemInfo(item);
        _itemInfoRenderer.SetPrice(_priceDescription, item.Price);
        UpdateButton(MoneyManager.Instance.MoneyCount);
    }

    protected virtual void UpdateButton(int moneyCount)
    {
        _buyButton.interactable = moneyCount >= _item.Price;
    }

    public override void Destroy()
    {
        MoneyManager.Instance.MoneyChanged -= UpdateButton;
        base.Destroy();
    }

    protected override void SetButtonListener(BaseShop shop)
    {
        _buyButton.onClick.RemoveAllListeners();
        _buyButton.onClick.AddListener(delegate { shop.BuyItem(_item); });
    }
}
