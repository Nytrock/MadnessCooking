using System;
using UnityEngine;

public class FoodManager : SaveableItemManager<Food, KitchenData> {
    [SerializeField] private CafeStateChanger _cafeOpener;

    private FoodManagerData _foodData;

    public int FoodCountWithoutDefault => Mathf.Max(0, AvailableItemsCount - _defaultItems.Count);

    public event Action<MenuFood> MenuFoodAdded;

    private void Awake() {
        _cafeOpener.CafeChanged += UpdateMenu;
    }

    public override void LateStart() {
        base.LateStart();
        CheckIsDataOld();
    }

    public Food GetFoodForOrder() {
        return _foodData.GetFoodForOrder();
    }

    public override void AddItem(Food item) {
        if (_data.IsItemAvailable(item))
            return;

        base.AddItem(item);
        AddMenuFood(item);
    }

    private void AddMenuFood(Food item) {
        MenuFood menuFood = _foodData.CreateMenuFood(item);
        MenuFoodAdded?.Invoke(menuFood);
    }

    public override void Bind(KitchenData data) {
        data.FoodManager ??= new();
        _data = data.FoodManager;
        _foodData = data.FoodManager;
    }

    private void CheckIsDataOld() {
        if (!(_foodData.FoodMenuLength == 0 && _data.ItemsCount != 0))
            return;

        foreach (var food in _data.AvailableItems)
            AddMenuFood(food);
        _foodData.GenerateNowFoodMenu();
    }

    private void UpdateMenu(bool isOpened) {
        if (!isOpened)
            return;

        _foodData.UpdateNowFoodMenu();
    }
}
