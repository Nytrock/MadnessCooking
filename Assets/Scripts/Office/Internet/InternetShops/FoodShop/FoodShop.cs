using System.Collections.Generic;
using UnityEngine;

public class FoodShop : BaseChooseShop
{
    [SerializeField] private List<Food> _foodToBuy;
    [SerializeField] private FoodManager _foodManager;

    public override void BuyItem(BuyableObject item)
    {
        base.BuyItem(item);

        var food = item as Food;
        if (food == null)
            return;

        MoneyManager.instance.ChangeMoney(-food.Cost);
        _foodManager.AddFood(food);

        var index = _foodToBuy.IndexOf(food);
        _foodToBuy.RemoveAt(index);
        _catalog.RemovePanel(index);
        SetObjectsArray();
    }

    protected override void SetObjectsArray()
    {
        _itemsToBuy = _foodToBuy.ToArray();
    }
}
