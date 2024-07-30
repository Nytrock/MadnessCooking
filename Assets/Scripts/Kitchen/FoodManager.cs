using UnityEngine;

public class FoodManager : SaveableItemManager<Food, KitchenData> {
    public int FoodCount => _data.ItemsCount;

    public Food GetRandomFood() {
        return _data.GetItem(Random.Range(0, FoodCount));
    }

    public override void Bind(KitchenData data) {
        data.FoodManager ??= new();
        _data = data.FoodManager;
        base.Bind(data);
    }
}
