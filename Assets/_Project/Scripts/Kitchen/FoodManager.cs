using UnityEngine;

public class FoodManager : SaveableItemManager<Food, KitchenData> {
    public int AllFoodCount => _data.ItemsCount;
    public int NoDefaultFoodCount => Mathf.Max(0, AllFoodCount - _defaultItems.Count);

    public Food GetRandomFood() {
        return _data.GetItem(Random.Range(0, AllFoodCount));
    }

    public override void Bind(KitchenData data) {
        data.FoodManager ??= new();
        _data = data.FoodManager;
    }
}
