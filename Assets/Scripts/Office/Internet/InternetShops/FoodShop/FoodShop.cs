using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodShop : BaseChooseShop, IBindable<OfficeData>
{
    [SerializeField] private List<Food> _foodToBuy;
    [SerializeField] private FoodManager _foodManager;
    private OfficeData _data;

    public override Type Type => typeof(Food);

    public override void BuyItem(BuyableObject item)
    {
        base.BuyItem(item);

        var food = item as Food;
        if (food == null)
            throw new NullReferenceException($"Buying item is not {Type}");

        MoneyManager.Instance.ChangeMoney(-food.Price);
        _foodManager.AddFood(food);

        int index = _foodToBuy.IndexOf(food);
        _foodToBuy.RemoveAt(index);
        _catalog.RemovePanel(index);
        SetObjectsArray();
    }

    protected override void SetObjectsArray()
    {
        _foodToBuy = _foodToBuy.OrderBy(x => x.Price).ToList();
        _data.ShopFood = _foodToBuy.ToArray();
        _itemsToBuy = _data.ShopFood;
    }

    public void Bind(OfficeData data, bool isFileEmpty)
    {
        _data = data;
        if (!isFileEmpty)
            _foodToBuy = _data.ShopFood.ToList();
        LateStart();
    }
}
