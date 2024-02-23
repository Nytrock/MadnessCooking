public class IngredientBuyPanel : BaseInstantBuyPanel
{
    protected override void OnBuyItem()
    {
        if (((Ingredient)_item).TypeIngredient != IngredientType.Buyable)
            Destroy();
    }
}
