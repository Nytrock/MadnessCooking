using System;
using System.Collections;
using UnityEngine;

public class Chickens : MonoBehaviour, IUpgradeable
{
    [SerializeField] private FarmCar _car;
    private int _feedCount = 0;
    private float _speed = 1; 

    [SerializeField] private float _maxFeedTime;
    [SerializeField] private float _eggTime;
    private float _nowTime;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _food;
    [SerializeField] private BaseUpgrade _infiniteFood;
    [SerializeField, Min(1)] private float _foodSpeedCoef;

    public bool IsFeed { get; private set; }
    public bool IsInfiniteFood { get; private set; }
    public int EggCount { get; private set; }
    public int FoodCount { get; private set; }

    public float EggTime => _eggTime;
    public float NowTime => _nowTime;

    public event Action EggChanged;
    public event Action FoodCountChanged;

    private void Start()
    {
        EggChanged?.Invoke();
        FoodCountChanged?.Invoke();
    }

    private void Update()
    {
        if (!IsFeed)
            return;

        if (_nowTime < _eggTime) { 
            _nowTime += Time.deltaTime * TimeManager.instance.TimeSpeed * _speed;
        } else { 
            _nowTime = 0;
            AddEgg();
        }
    }

    public void Feed()
    {
        _feedCount++;
        _speed += _foodSpeedCoef / _feedCount;
        StartCoroutine(FeedTimer());
        IsFeed = true;

        if (IsInfiniteFood)
            return;

        FoodCount--;
        FoodCountChanged?.Invoke();
    }

    private IEnumerator FeedTimer()
    {
        yield return new WaitForSeconds(_maxFeedTime * _foodSpeedCoef / _feedCount);
        _speed -= _foodSpeedCoef / _feedCount;
        _feedCount--;
        if (_feedCount == 0)
            IsFeed = false;
    }

    public void AddEgg()
    {
        EggCount++;
        EggChanged?.Invoke();
    }

    public void EggsToCar(Ingredient egg)
    {
        FatigueManager.instance.ChangeFatigue(egg.FatigueCount * EggCount);
        _car.PutIngredient(new IngredientCount(egg, EggCount));
        EggCount = 0;
        EggChanged?.Invoke();
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (upgrade == _food)
            AddFood();
        else if (upgrade == _infiniteFood)
            SetInfiniteFood();
    }

    private void SetInfiniteFood()
    {
        FoodCount = -1;
        IsInfiniteFood = true;
        FoodCountChanged?.Invoke();
    }

    private void AddFood()
    {
        FoodCount++;
        FoodCountChanged?.Invoke();
    }
}
