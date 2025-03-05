using UnityEngine;

public class FoodManager : SaveableItemManager<Food, KitchenData> {
    private FoodManagerData _foodData => _data as FoodManagerData;
    public int AllFoodCount => _data.ItemsCount;
    public int FoodCountWithoutDefault => Mathf.Max(0, AllFoodCount - _defaultItems.Count);

    public Food GetFoodForOrder() {
        return _foodData.GetFoodForOrder();
    }

    public override void Bind(KitchenData data) {
        data.FoodManager ??= new();
        _data = data.FoodManager;
    }
}
