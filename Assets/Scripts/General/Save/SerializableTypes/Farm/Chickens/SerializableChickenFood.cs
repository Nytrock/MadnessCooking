using System;
using UnityEngine;

[Serializable]
public class SerializableChickenFood
{
    [SerializeField] private float _nowTime;
    [SerializeField] private float _needTime;
    [SerializeField] private float _foodCoef;

    public bool IsEnded => _nowTime >= _needTime;
    public float FoodCoef => _foodCoef;

    public SerializableChickenFood(float basicTime, float foodCoef)
    {
        _foodCoef = foodCoef;
        _needTime = basicTime * _foodCoef;
    }

    public void AddTime()
    {
        _nowTime += Time.deltaTime;
    }
}
