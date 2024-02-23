using UnityEngine;

public class FoodShopItemView : BaseChooseShopItemView
{
    [SerializeField] private FoodRecipe _recipeRenderer;

    protected override void SetInfo(BuyableObject item)
    {
        base.SetInfo(item);

        var food = item as Food;
        _recipeRenderer.SetupRecipe(food);
    }

    public override void ResetInfo()
    {
        base.ResetInfo();
        _recipeRenderer.DisableParts();
    }
}
