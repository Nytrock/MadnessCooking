using UnityEngine;

public abstract class BaseInstantBuyPanel : BaseBuyPanel
{
    [SerializeField] private ItemInfoRendererWithCost _itemInfoRenderer;
    [SerializeField] protected string _costDescription;

    public override void Setup(BuyableObject item, BaseShop shop)
    {
        base.Setup(item, shop);
        MoneyManager.Instance.MoneyChanged += UpdateButton;
    }

    public override void SetVisual(BuyableObject item)
    {
        _itemInfoRenderer.SetItemInfo(item);
        _itemInfoRenderer.SetCost(_costDescription, item.Cost);
        UpdateButton(MoneyManager.Instance.MoneyCount);
    }

    protected virtual void UpdateButton(int moneyCount)
    {
        _buyButton.interactable = moneyCount >= _item.Cost;
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
