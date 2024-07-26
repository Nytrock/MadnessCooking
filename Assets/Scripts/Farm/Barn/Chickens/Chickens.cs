using System;
using System.Collections.Generic;
using UnityEngine;

public class Chickens : MonoBehaviour, IBindable<FarmData> {
    [SerializeField] private FarmCar _car;
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private IngredientsManager _ingredientsManager;
    [SerializeField] private Puncher _puncher;
    [SerializeField, Min(0)] private float _baseWasteAmount;
    [SerializeField, Min(0)] private float _maxFoodWorkTime;
    [SerializeField, Min(0)] private float _eggTime;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _unlockUpgrade;
    [SerializeField] private BaseUpgrade _food;
    [SerializeField] private BaseUpgrade _infiniteFood;
    [SerializeField, Min(1)] private float _foodSpeedCoef;

    private Ingredient _egg;

    public float EggTime => _eggTime;

    public ChickensData Data { get; private set; }

    public event Action<int> FoodCountChanged;
    public event Action<int> EggCountChanged;

    private void Awake() {
        _upgradeManager.ItemAdded += CheckUpgrades;
    }

    private void LateStart() {
        _egg = ConstIngredients.Instance.Egg;
        FoodCountChanged?.Invoke(Data.FoodCount);
        ChangeState();
    }

    private void Update() {
        if (!Data.IsFeed)
            return;

        UpdateFoods();
        _puncher.AddWaste(_baseWasteAmount * Data.Speed);
        if (Data.NowTime < _eggTime) {
            Data.UpdateTime();
        } else {
            Data.AddEgg();
            EggCountChanged?.Invoke(Data.EggCount);
        }
    }

    private void UpdateFoods() {
        List<ChickenFoodData> foodToRemove = new();
        foreach (var food in Data.FoodList) {
            food.AddTime();
            if (food.IsEnded)
                foodToRemove.Add(food);
        }

        foreach (var item in foodToRemove)
            RemoveFood(item);
        foodToRemove.Clear();
    }

    private void RemoveFood(ChickenFoodData food) {
        Data.RemoveFood(food);
    }

    public void Feed() {
        float foodCoef = _foodSpeedCoef / (Data.UsedFoodCount + 1);
        ChickenFoodData newFood = new(_maxFoodWorkTime, foodCoef);
        Data.AddFood(newFood);

        FoodCountChanged?.Invoke(Data.FoodCount);
    }

    public void EggsToCar() {
        int remainCount = _car.PutIngredientWithRemain(_egg, Data.EggCount);
        FatigueManager.Instance.ChangeFatigue(_egg.FatigueCoef * (Data.EggCount - remainCount));
        Data.SetEggCount(remainCount);
        EggCountChanged?.Invoke(remainCount);
    }

    public void CheckUpgrades(BaseUpgrade upgrade) {
        if (upgrade == _unlockUpgrade) {
            Data.Unlock();
            ChangeState();
        } else if (upgrade == _food) {
            AddFood();
        } else if (upgrade == _infiniteFood) {
            SetInfiniteFood();
        }
    }

    private void ChangeState() {
        gameObject.SetActive(Data.IsUnlocked);
        if (Data.IsUnlocked)
            _ingredientsManager.AddItem(_egg);
    }

    private void SetInfiniteFood() {
        Data.SetInfiniteFood();
        FoodCountChanged?.Invoke(Data.FoodCount);
    }

    private void AddFood() {
        Data.AddFood();
        FoodCountChanged?.Invoke(Data.FoodCount);
    }

    public void Bind(FarmData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.Chickens = new();
        Data = data.Chickens;
        LateStart();
    }
}
