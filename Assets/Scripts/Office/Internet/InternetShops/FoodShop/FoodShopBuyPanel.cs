using System;

public class FoodShopBuyPanel : BaseChooseBuyPanel
{
    public override Type Type => typeof(Food);

    protected override void OnChooseItem()
    {
        // Change face
    }

    public override void BuyChosenItem()
    {
        Destroy();
    }
}
