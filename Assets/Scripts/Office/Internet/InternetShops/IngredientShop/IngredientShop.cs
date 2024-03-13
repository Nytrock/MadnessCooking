using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IngredientShop : BaseInstantShop
{
    [SerializeField] private List<Ingredient> _ingredientsToBuy;
    [SerializeField] private IngredientsManager _ingredientsManager;
    [SerializeField] private KitchenStorage _ingredientStorage;

    public override void BuyItem(BuyableObject item)
    {
        var ingredient = item as Ingredient;
        if (ingredient == null)
            return;

        MoneyManager.instance.ChangeMoney(-ingredient.Cost);
        if (ingredient.TypeIngredient == IngredientType.Buyable) {
            _ingredientStorage.PutIngredient(new IngredientCount(ingredient, 1));
            return;
        } else {
            _ingredientsManager.AddIngredient(ingredient);
        }

        var index = _ingredientsToBuy.IndexOf(ingredient);
        _ingredientsToBuy.RemoveAt(index);
        _catalog.RemovePanel(index);
        SetObjectsArray();
    }

    protected override void SetObjectsArray()
    {
        _ingredientsToBuy = _ingredientsToBuy.OrderBy(x => 
        (x.TypeIngredient != IngredientType.Buyable, x.Cost)).ToList();
        _itemsToBuy = _ingredientsToBuy.ToArray();
    }
}
