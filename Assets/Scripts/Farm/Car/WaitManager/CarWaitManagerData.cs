using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CarWaitManagerData {
    [SerializeField] private IngredientCountList _ingredientsSended = new();
    [SerializeField] private CarState _carState = CarState.Calm;
    [SerializeField] private float _nowWaitTime;
    [SerializeField] private float _needWaitTime;

    public IEnumerable<IngredientCount> IngredientsSended => _ingredientsSended.GetIngredients();
    public CarState CarState => _carState;
    public float NowWaitTime => _nowWaitTime;
    public float NeedWaitTime => _needWaitTime;

    public CarWaitManagerData(float defaultWaitTime) {
        _needWaitTime = defaultWaitTime;
    }

    public void UpdateSpeed(CountUpgrade countUpgrade) {
        _needWaitTime = countUpgrade.Count;
        if (_carState != CarState.Calm) {
            _nowWaitTime = Mathf.Min(_nowWaitTime, _needWaitTime);
        }
    }

    public void UpdateTime() {
        _nowWaitTime -= InGameTime.Instance.DeltaTime;
    }

    public void StartWait() {
        _nowWaitTime = _needWaitTime;
        _carState = CarState.Sent;
    }

    public void SetIngredientsSended(IngredientCountList ingredients) {
        _ingredientsSended.Clear();
        _ingredientsSended.Extend(ingredients);
    }

    public void StartReturn() {
        _carState = CarState.Returns;
        _ingredientsSended.Clear();
        _nowWaitTime = _needWaitTime;
    }

    public void StartCalm() {
        _carState = CarState.Calm;
    }
}
