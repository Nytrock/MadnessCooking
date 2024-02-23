public abstract class BaseChooseBuyPanel : BaseBuyPanel
{
    public BuyableObject Item => _item;

    protected override void SetButtonListener(BaseShop shop)
    {
        var chooseShop = shop as BaseChooseShop;
        _buyButton.onClick.AddListener(delegate { chooseShop.ChooseItem(this); OnChooseItem(); });
    }

    protected abstract void OnChooseItem();
    public abstract void BuyChosenItem();
}
