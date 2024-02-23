using UnityEngine;

public abstract class BaseChooseBuyPanel : BaseBuyPanel
{
    [SerializeField] private ItemInfoRenderer _itemInfoRenderer;
    public BuyableObject Item => _item;

    public override void Setup(BuyableObject item, BaseShop shop)
    {
        _itemInfoRenderer.SetItemInfo(item);
        base.Setup(item, shop);
    }

    protected override void SetButtonListener(BaseShop shop)
    {
        var chooseShop = shop as BaseChooseShop;
        _buyButton.onClick.AddListener(delegate { chooseShop.ChooseItem(this); OnChooseItem(); });
    }

    protected abstract void OnChooseItem();
    public abstract void BuyChosenItem();
}
