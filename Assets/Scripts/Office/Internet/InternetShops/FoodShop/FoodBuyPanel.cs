public class FoodBuyPanel : BaseChooseBuyPanel
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
