using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodShop : BaseChooseShop
{
    [SerializeField] private List<Food> _foodToBuy;
    [SerializeField] private FoodManager _foodManager;

    public override Type Type => typeof(Food);

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
        _foodToBuy = _foodToBuy.OrderBy(x => x.Cost).ToList();
        _itemsToBuy = _foodToBuy.ToArray();
    }
}
