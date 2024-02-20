using UnityEngine;

public class IngredientShop : BaseInternetShop
{
    [SerializeField] private Ingredient[] _ingredientsToBuy;
    [SerializeField] private IngredientsManager _ingredientsManager;

    protected override void BuyItem(int itemIndex)
    {

    }

    protected override void SetObjectsArray()
    {
        _itemsToBuy = (BuyableObject[]) _ingredientsToBuy.Clone();
    }
}
