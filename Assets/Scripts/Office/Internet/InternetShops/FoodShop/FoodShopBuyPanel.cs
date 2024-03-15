public class FoodShopBuyPanel : BaseChooseBuyPanel
{
    protected override void OnChooseItem()
    {
        // Change face
    }

    public override void BuyChosenItem()
    {
        Destroy();
    }
}
