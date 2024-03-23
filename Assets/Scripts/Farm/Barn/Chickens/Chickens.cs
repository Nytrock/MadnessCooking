using System;
using UnityEngine;

public class Chickens : MonoBehaviour, IUpgradeable
{
    [SerializeField] private FarmCar _car;

    private bool _isFeed;
    private bool _isAuto;

    [SerializeField] private float _maxFeedTime;
    private float _feedTime;

    [SerializeField] private float _eggTime;
    private float _nowTime;
    private int _eggCount;
    
    public float EggTime => _eggTime;
    public float NowTime => _nowTime;

    public event Action<int> EggChanged;

    private void Update()
    {
        if (!_isFeed && !_isAuto)
            return;

        if (_nowTime < _eggTime) { 
            _nowTime += Time.deltaTime * TimeManager.instance.TimeSpeed;
        } else { 
            _nowTime = 0;
            AddEgg();
        }

        if (_feedTime < _maxFeedTime) { 
            _feedTime += Time.deltaTime * TimeManager.instance.TimeSpeed;
        } else {
            _isFeed = false;
        }
    }

    public void Feed()
    {
        _feedTime = 0;
        _isFeed = true;
    }

    public void AddEgg()
    {
        _eggCount++;
        EggChanged?.Invoke(_eggCount);
    }

    public void EggsToCar(Ingredient egg)
    {
        _car.PutIngredient(new IngredientCount(egg, _eggCount));
        _eggCount = 0;
        EggChanged?.Invoke(_eggCount);
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {

    }
}
