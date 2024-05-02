using UnityEngine;

public abstract class BaseChooseBuyPanel : BaseBuyPanel
{
    [SerializeField] private ItemInfoRenderer _itemInfoRenderer;
    public BuyableObject Item => _item;

    public override void SetVisual(BuyableObject item)
    {
        _itemInfoRenderer.SetItemInfo(item);
    }

    protected override void SetButtonListener(BaseShop shop)
    {
        var chooseShop = shop as BaseChooseShop;
        _buyButton.onClick.RemoveAllListeners();
        _buyButton.onClick.AddListener(delegate { chooseShop.ChooseItem(this); OnChooseItem(); });
    }

    protected abstract void OnChooseItem();
    public abstract void BuyChosenItem();
}
