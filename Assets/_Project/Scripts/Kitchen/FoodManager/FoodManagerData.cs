using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class FoodManagerData : BuyableItemManagerData<Food> {
    [SerializeField, JsonProperty] private int _foodForOrderIndex = -1;

    public Food GetFoodForOrder() {
        _foodForOrderIndex = (_foodForOrderIndex + 1) % ItemsCount;
        if (_foodForOrderIndex == 0)
            _availableItems.Randomize();
        return _availableItems[_foodForOrderIndex];
    }
}
