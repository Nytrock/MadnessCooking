using System;
using System.Collections.Generic;
using UnityEngine;
using MadnessCooking.Cafe;
using MadnessCooking.General;

namespace MadnessCooking.Kitchen {
    public class FoodManager : SaveableItemManager<Food, KitchenData> {
        [SerializeField] private CafeStateChanger _cafeOpener;

        private FoodManagerData _foodData;

        public int NowFoodMenuCountWithoutDefault => Mathf.Max(0, _foodData.NowFoodMenuLength - _defaultItems.Count);
        public IEnumerable<MenuFood> FoodMenu => _foodData.FoodMenu;

        public event Action<MenuFood> MenuFoodAdded;

        private void Awake() {
            _cafeOpener.CafeChanged += UpdateMenu;
        }

        public override void LateStart() {
            base.LateStart();
            CheckIsDataOld();

            foreach (var menuFood in _foodData.FoodMenu)
                menuFood.BanishedStateChanged += delegate { _foodData.CheckMenuSize(menuFood); };
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
            menuFood.BanishedStateChanged += delegate { _foodData.CheckMenuSize(menuFood); };
            MenuFoodAdded?.Invoke(menuFood);
        }

        public override void Bind(KitchenData data) {
            data.FoodManager ??= new(_defaultItems);
            _data = data.FoodManager;
            _foodData = data.FoodManager;
        }

        private void CheckIsDataOld() {
            if (!(_foodData.FoodMenuLength == 0 && _data.ItemsCount != 0))
                return;

            foreach (var food in _data.AvailableItems)
                _foodData.CreateMenuFood(food);
            _foodData.GenerateNowFoodMenu();
        }

        private void UpdateMenu(bool isOpened) {
            if (!isOpened)
                return;

            _foodData.UpdateNowFoodMenu();
        }
    }
}
