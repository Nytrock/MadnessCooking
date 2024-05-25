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

    public float EggTime => _eggTime;

    public SerializableChickens Data { get; private set; }

    public event Action FoodCountChanged;

    private void LateStart()
    {
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
        List<SerializableChickenFood> foodToRemove = new();
        foreach (var food in Data.FoodList) {
            food.AddTime();
            if (food.IsEnded)
                foodToRemove.Add(food);
        }

        foreach (var item in foodToRemove)
            RemoveFood(item);
        foodToRemove.Clear();
    }

    private void RemoveFood(SerializableChickenFood food)
    {
        Data.FoodList.Remove(food);
        Data.Speed -= food.FoodCoef;
        if (Data.FoodList.Count == 0)
            Data.IsFeed = false;
    }

    public void Feed()
    {
        float foodCoef = _foodSpeedCoef / (Data.FoodList.Count + 1);
        Data.FoodList.Add(new SerializableChickenFood(_maxFoodWorkTime, foodCoef));

        Data.Speed += foodCoef;
        Data.IsFeed = true;

        if (Data.IsInfiniteFood)
            return;

        Data.FoodCount--;
        FoodCountChanged?.Invoke();
    }

    public void EggsToCar()
    {
        Ingredient egg = IngredientsManager.Instance.Egg;
        FatigueManager.Instance.ChangeFatigue(egg.FatigueCount * Data.EggCount);
        _car.PutIngredient(new IngredientCount(egg, Data.EggCount));
        Data.EggCount = 0;
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
