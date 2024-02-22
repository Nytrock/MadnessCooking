using System;
using UnityEngine;

public class Chickens : MonoBehaviour
{
    [SerializeField] private Car _car;

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
            _nowTime += Time.deltaTime;
        } else { 
            _nowTime = 0;
            AddEgg();
        }

        if (_feedTime < _maxFeedTime) { 
            _feedTime += Time.deltaTime;
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
}
