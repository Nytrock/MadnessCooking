using System;
using System.Collections.Generic;
using UnityEngine;

public class BarnChickens : MonoBehaviour, IBindable<FarmData> {
    [SerializeField] private FarmCar _car;
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private IngredientManager _ingredientsManager;
    [SerializeField] private Puncher _puncher;
    [SerializeField] private FarmShop _farmShop;
    [SerializeField] private VisualChanger _unlockVisual;
    [SerializeField, Min(0)] private float _baseWasteAmount;
    [SerializeField, Min(0)] private float _maxFoodWorkTime;
    [SerializeField, Min(0)] private float _eggTime;
    [SerializeField, Min(0)] private int _defaultFoodCount;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _unlockUpgrade;
    [SerializeField] private ConsumableUpgrade _food;
    [SerializeField] private BaseUpgrade _infiniteFood;
    [SerializeField, Min(1)] private float _foodSpeedCoef;

    private Ingredient _egg;

    public float EggTime => _eggTime;

    public ChickensData Data { get; private set; }

    public event Action FoodCountChanged;
    public event Action EggCountChanged;
    public event Action SpeedChanged;
    public event Action FeedStateChanged;

    private void Awake() {
        _farmShop.ConsumableUpgradeBuyed += CheckConsumableUpgrade;
        _upgradeManager.ItemAdded += CheckUpgrades;
    }

    private void CheckConsumableUpgrade(ConsumableUpgrade upgrade) {
        if (upgrade != _food) return;
        AddFood();
    }

    public void LateStart() {
        _egg = ConstIngredients.Instance.Egg;
        UpdateUnlockState();

        InvokeFeedRelatedActions();
        FoodCountChanged?.Invoke();
        EggCountChanged?.Invoke();
    }

    private void Update() {
        if (!Data.IsFeed || !Data.IsUnlocked)
            return;

        UpdateFoods();
        _puncher.AddWaste(_baseWasteAmount * Data.Speed);
        if (Data.NowTime < _eggTime) {
            Data.UpdateTime();
        } else {
            Data.AddEgg();
            EggCountChanged?.Invoke();
        }
    }

    private void UpdateFoods() {
        List<ChickenFoodData> foodToRemove = new();
        foreach (var food in Data.FoodList) {
            food.AddTime();
            if (food.IsEnded)
                foodToRemove.Add(food);
        }

        if (foodToRemove.Count > 0) {
            foreach (var item in foodToRemove)
                RemoveFood(item);
            foodToRemove.Clear();
            InvokeFeedRelatedActions();
        }
    }

    private void RemoveFood(ChickenFoodData food) {
        Data.RemoveFood(food);
    }

    public void Feed() {
        float foodCoef = _foodSpeedCoef / (Data.UsedFoodCount + 1);
        ChickenFoodData newFood = new(_maxFoodWorkTime, foodCoef);
        Data.AddFood(newFood);

        FoodCountChanged?.Invoke();
        InvokeFeedRelatedActions();
    }

    public void EggsToCar() {
        int remainCount = _car.PutIngredientWithRemain(_egg, Data.EggCount);
        FatigueManager.Instance.AddFatigue(_egg.FatigueCoef * (Data.EggCount - remainCount));
        Data.SetEggCount(remainCount);
        EggCountChanged?.Invoke();
    }

    public void CheckUpgrades(BaseUpgrade upgrade) {
        if (upgrade == _unlockUpgrade) {
            Data.Unlock();
            UpdateUnlockState();
        } else if (upgrade == _infiniteFood) {
            SetInfiniteFood();
        }
    }

    private void UpdateUnlockState() {
        _unlockVisual.ChangeState(Data.IsUnlocked);
        if (Data.IsUnlocked)
            _ingredientsManager.AddItem(_egg);
    }

    private void SetInfiniteFood() {
        Data.SetInfiniteFood();
        FoodCountChanged?.Invoke();
    }

    private void AddFood() {
        Data.AddFood();
        FoodCountChanged?.Invoke();
    }

    public void Bind(FarmData data) {
        data.Chickens ??= new(_defaultFoodCount);
        Data = data.Chickens;
    }

    private void InvokeFeedRelatedActions() {
        SpeedChanged?.Invoke();
        FeedStateChanged?.Invoke();
    }
}
