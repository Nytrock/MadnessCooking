using UnityEngine;

public abstract class BaseInstantBuyPanel : BaseBuyPanel
{
    [SerializeField] private ItemInfoRendererWithNum _itemInfoRenderer;
    [SerializeField] protected string _costText;

    public override void Setup(BuyableObject item, BaseShop shop)
    {
        base.Setup(item, shop);
        _itemInfoRenderer.SetItemInfo(item);
        _itemInfoRenderer.SetNumText(_costText + item.Cost);
        UpdateButton(MoneyManager.instance.MoneyAmount);
        MoneyManager.instance.MoneyChanged += UpdateButton;
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
        _buyButton.onClick.AddListener(delegate { instantShop.BuyItem(_item); OnBuyItem(); });
    }

    protected abstract void OnBuyItem();
}
