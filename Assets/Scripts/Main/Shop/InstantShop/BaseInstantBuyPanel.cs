using UnityEngine;

public class BaseInstantBuyPanel : BaseBuyPanel
{
    [SerializeField] private ItemInfoRendererWithNum _itemInfoRenderer;
    [SerializeField] protected string _costText;

    public override void Setup(BuyableObject item, BaseShop shop)
    {
        base.Setup(item, shop);
        MoneyManager.instance.MoneyChanged += UpdateButton;
    }

    public override void SetVisual(BuyableObject item)
    {
        _itemInfoRenderer.SetItemInfo(item);
        _itemInfoRenderer.SetNumText(_costText + item.Cost);
        UpdateButton(MoneyManager.instance.MoneyAmount);
    }

    public void UpdateButton(int moneyCount)
    {
        _buyButton.interactable = moneyCount >= _item.Cost;
    }

    public override void Destroy()
    {
        MoneyManager.instance.MoneyChanged -= UpdateButton;
        base.Destroy();
    }

    protected override void SetButtonListener(BaseShop shop)
    {
        var instantShop = shop as BaseInstantShop;
        _buyButton.onClick.RemoveAllListeners();
        _buyButton.onClick.AddListener(delegate { instantShop.BuyItem(_item); });
    }
}
