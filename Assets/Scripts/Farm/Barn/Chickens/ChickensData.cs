using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChickensData {
    [SerializeField] private List<ChickenFoodData> _foodList;
    [SerializeField] private bool _isUnlocked;
    [SerializeField] private float _nowTime;
    [SerializeField] private float _speed;

    [SerializeField] private int _eggCount;
    [SerializeField] private int _foodCount;
    [SerializeField] private bool _isFeed;
    [SerializeField] private bool _isInfiniteFood;

    public IEnumerable<ChickenFoodData> FoodList => _foodList;
    public int UsedFoodCount => _foodList.Count;
    public bool IsUnlocked => _isUnlocked;
    public float NowTime => _nowTime;
    public float Speed => _speed;
    public int EggCount => _eggCount;
    public int FoodCount => _foodCount;
    public bool IsFeed => _isFeed;
    public bool IsInfiniteFood => _isInfiniteFood;

    public ChickensData(int foodCount) {
        _foodList = new();
        _foodCount = foodCount;
        _speed = 0;
    }

    public void UpdateTime() {
        _nowTime += InGameTime.Instance.NormalizedDeltaTime * _speed;
    }

    public void AddEgg() {
        _nowTime = 0;
        _eggCount++;
    }

    public void AddFood() {
        _foodCount++;
    }

    public void RemoveFood(ChickenFoodData expiredFood) {
        _foodList.Remove(expiredFood);
        _speed -= expiredFood.FoodCoef;
        if (_foodList.Count == 0)
            _isFeed = false;
    }

    public void AddFood(ChickenFoodData newFood) {
        _foodList.Add(newFood);

        _speed += newFood.FoodCoef;
        _isFeed = true;

        if (_isInfiniteFood)
            return;

        _foodCount--;
    }

    public void SetEggCount(int count) {
        if (count < 0)
            return;

        _eggCount = count;
    }

    public void Unlock() {
        _isUnlocked = true;
    }

    public void SetInfiniteFood() {
        _foodCount = -1;
        _isInfiniteFood = true;
    }
}
