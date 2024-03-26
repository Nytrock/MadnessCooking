using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IngredientShop : BaseInstantShop, IUpgradeable
{
    [SerializeField] private List<Ingredient> _ingredientsToBuy;
    [SerializeField] private IngredientsManager _ingredientsManager;
    [SerializeField] private BedTypesManager _bedTypesManager;
    [SerializeField] private KitchenStorage _ingredientStorage;

    [Header("Upgrades")]
    [SerializeField] private Ingredient _spice;
    [SerializeField] private BaseUpgrade _spiceAutoBuy;

    public BedTypesManager BedTypesManager => _bedTypesManager;
    public override Type Type => typeof(Ingredient);

    public override void BuyItem(BuyableObject item)
    {
        var ingredient = item as Ingredient;
        if (ingredient == null)
            return;

        MoneyManager.instance.ChangeMoney(-ingredient.Cost);
        if (ingredient.Type == IngredientType.Buyable) {
            _ingredientStorage.PutIngredient(new IngredientCount(ingredient, 1));
            return;
        } else {
            _ingredientsManager.AddIngredient(ingredient);
        }

        RemoveIngredient(ingredient);
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (upgrade == _spiceAutoBuy)
            RemoveIngredient(_spice);
    }

    private void RemoveIngredient(Ingredient ingredient)
    {
        var index = _ingredientsToBuy.IndexOf(ingredient);
        _ingredientsToBuy.RemoveAt(index);
        _catalog.RemovePanel(index);
        SetObjectsArray();
    }

    protected override void SetObjectsArray()
    {
        _ingredientsToBuy = _ingredientsToBuy.OrderBy(x => 
        (x.Type != IngredientType.Buyable, x.Cost)).ToList();
        _itemsToBuy = _ingredientsToBuy.ToArray();
    }
}
