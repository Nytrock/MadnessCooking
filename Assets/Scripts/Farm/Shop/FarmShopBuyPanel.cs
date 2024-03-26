using System;

public class FarmShopBuyPanel : BaseChooseBuyPanel
{
    public override Type Type => typeof(BaseUpgrade);

    protected override void OnChooseItem()
    {
        // Change face
    }

    public override void BuyChosenItem()
    {

    }

    protected override void SetButtonListener(BaseShop shop)
    {
        base.SetButtonListener(shop);
        var chooseShop = shop as BaseChooseShop;
        if (chooseShop.NowPanel == this)
            chooseShop.ChooseItem(this);
    }
}
