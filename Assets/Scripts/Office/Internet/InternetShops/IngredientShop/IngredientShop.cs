using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IngredientShop : BaseInstantShop, IUpgradeable, IBindable<OfficeData>
{
    [SerializeField] private List<Ingredient> _ingredientsToBuy;
    [SerializeField] private IngredientsManager _ingredientsManager;
    [SerializeField] private BedTypesManager _bedTypesManager;
    [SerializeField] private KitchenStorage _ingredientStorage;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _spiceAutoBuy;

    private OfficeData _data;

    public BedTypesManager BedTypesManager => _bedTypesManager;
    public override Type Type => typeof(Ingredient);

    public override void BuyItem(BuyableObject item)
    {
        var ingredient = item as Ingredient;
        if (ingredient == null)
            throw new NullReferenceException($"Buying item is not {Type}");

        MoneyManager.Instance.ChangeMoney(-ingredient.Price);
        if (ingredient.Type == IngredientType.Buyable) {
            _ingredientStorage.PutIngredientWithRemain(new IngredientCount(ingredient, 1));
        } else {
            _ingredientsManager.AddIngredient(ingredient);
            RemoveIngredient(ingredient);
        }
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (upgrade == _spiceAutoBuy)
            RemoveIngredient(ConstIngredients.Instance.Spice);
    }

    private void RemoveIngredient(Ingredient ingredient)
    {
        int index = _ingredientsToBuy.IndexOf(ingredient);
        _ingredientsToBuy.RemoveAt(index);
        _catalog.RemovePanel(index);
        SetObjectsArray();
    }

    protected override void SetObjectsArray()
    {
        _ingredientsToBuy = _ingredientsToBuy.OrderBy(x => 
        (x.Type != IngredientType.Buyable, x.Price)).ToList();
        _data.ShopIngredients = _ingredientsToBuy.ToArray();
        _itemsToBuy = _data.ShopIngredients;
    }

    public void Bind(OfficeData data, bool isFileEmpty)
    {
        _data = data;
        if (!isFileEmpty)
            _ingredientsToBuy = _data.ShopIngredients.ToList();
        LateStart();
    }
}
