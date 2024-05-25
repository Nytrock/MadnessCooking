using System;
using System.Collections.Generic;
using UnityEngine;

public class Chickens : MonoBehaviour, IUpgradeable, IBindable<FarmData>
{
    [SerializeField] private FarmCar _car;
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

    public event Action FoodCountChanged;

    private void LateStart()
    {
        _egg = IngredientsManager.Instance.Egg;
        FoodCountChanged?.Invoke();
        ChangeState();
    }

    private void Update()
    {
        if (!Data.IsFeed)
            return;

        UpdateFoods();
        if (Data.NowTime < _eggTime) {
            Data.NowTime += TimeManager.Instance.InGameTimeSpeed * Data.Speed;
        } else {
            Data.NowTime = 0;
            Data.EggCount++;
        }
    }

    private void UpdateFoods()
    {
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

    private void RemoveFood(ChickenFoodData food)
    {
        Data.FoodList.Remove(food);
        Data.Speed -= food.FoodCoef;
        if (Data.FoodList.Count == 0)
            Data.IsFeed = false;
    }

    public void Feed()
    {
        float foodCoef = _foodSpeedCoef / (Data.FoodList.Count + 1);
        Data.FoodList.Add(new ChickenFoodData(_maxFoodWorkTime, foodCoef));

        Data.Speed += foodCoef;
        Data.IsFeed = true;

        if (Data.IsInfiniteFood)
            return;

        Data.FoodCount--;
        FoodCountChanged?.Invoke();
    }

    public void EggsToCar()
    {
        int remainCount = _car.PutIngredientWithRemain(new IngredientCount(_egg, Data.EggCount));
        FatigueManager.Instance.ChangeFatigue(_egg.FatigueCount * (Data.EggCount - remainCount));
        Data.EggCount = remainCount;
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (upgrade == _unlockUpgrade) {
            Data.IsUnlocked = true;
            ChangeState();
        } else if (upgrade == _food) {
            AddFood();
        } else if (upgrade == _infiniteFood) {
            SetInfiniteFood();
        }
    }

    private void ChangeState()
    {
        gameObject.SetActive(Data.IsUnlocked);
    }

    private void SetInfiniteFood()
    {
        Data.FoodCount = -1;
        Data.IsInfiniteFood = true;
        FoodCountChanged?.Invoke();
    }

    private void AddFood()
    {
        Data.FoodCount++;
        FoodCountChanged?.Invoke();
    }

    public void Bind(FarmData data, bool isFileEmpty)
    {
        Data = data.Chickens;
        LateStart();
    }
}
