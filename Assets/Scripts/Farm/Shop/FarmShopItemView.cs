public class FarmShopItemView : BaseChooseShopItemView
{
    public override void BuyChosen()
    {
        _shop.BuyItem(_itemToBuy);
        var farmShop = _shop as FarmShop;
        if (!farmShop.IsBuyedItemReplaced)
            ResetInfo();
    }
}
