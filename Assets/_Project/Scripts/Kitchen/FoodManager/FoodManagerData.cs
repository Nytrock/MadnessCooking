using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class FoodManagerData : BuyableItemManagerData<Food> {
    [SerializeField, JsonProperty] private int _foodForOrderIndex = 0;
    [SerializeField, JsonProperty] private List<MenuFood> _foodMenu = new();
    [SerializeField, JsonProperty] private List<int> _nowFoodMenu;

    public int FoodMenuLength => _foodMenu.Count;
    public IEnumerable<MenuFood> FoodMenu => _foodMenu;

    public FoodManagerData() {
        _nowFoodMenu = new();
    }

    public MenuFood CreateMenuFood(Food food) {
        MenuFood menuFood = new(food);
        _foodMenu.Add(menuFood);

        _foodMenu = _foodMenu.OrderBy(item => item.Food.Price).ToList();
        _nowFoodMenu.Add(_foodMenu.Count - 1);
        return menuFood;
    }

    public Food GetFoodForOrder() {
        Food foodToReturn = _availableItems[_nowFoodMenu[_foodForOrderIndex]];
        UpdateFoodForOrderIndex();
        return foodToReturn;
    }

    public void GenerateNowFoodMenu() {
        _nowFoodMenu.Clear();
        for (int i = 0; i < _foodMenu.Count; i++) {
            if (_foodMenu[i].IsBanished)
                continue;
            _nowFoodMenu.Add(i);
        }

        _nowFoodMenu.Randomize();
    }

    private void UpdateFoodForOrderIndex() {
        _foodForOrderIndex = (_foodForOrderIndex + 1) % _nowFoodMenu.Count;
        if (_foodForOrderIndex == 0)
            _nowFoodMenu.Randomize();
    }

    public void UpdateNowFoodMenu() {
        _foodForOrderIndex = 0;
        GenerateNowFoodMenu();
    }

    public void CheckMenuSize(MenuFood lastChangedMenuFood) {
        int foodMenuSize = _foodMenu
            .Where(foodMenu => !foodMenu.IsBanished)
            .Count();
        if (foodMenuSize > 0)
            return;

        lastChangedMenuFood.ChangeBanishedState();
    }
}
