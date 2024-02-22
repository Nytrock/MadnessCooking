public class IngredientBuyPanel : BaseBuyPanel
{
    protected override void BuyItem()
    {
        if (((Ingredient)_item).TypeIngredient != IngredientType.Buyable)
            Destroy();
    }
}
