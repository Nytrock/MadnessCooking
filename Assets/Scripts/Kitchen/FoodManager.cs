using UnityEngine;

public class FoodManager : BuyableItemManager<Food>, IBindable<KitchenData> {
    public int FoodCount => _data.ItemsCount;

    public Food GetRandomFood() {
        return _data.GetItem(Random.Range(0, FoodCount));
    }

    public void Bind(KitchenData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.FoodManager = new(_defaultItems);
        _data = data.FoodManager;
    }
}
