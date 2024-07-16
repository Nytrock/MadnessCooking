using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CarWaitManagerData {
    [SerializeField] private BuyableItemCountList<Ingredient> _ingredientsSended;
    [SerializeField] private CarState _carState;
    [SerializeField] private float _nowWaitTime;
    [SerializeField] private float _needWaitTime;

    public IEnumerable<BuyableItemCount<Ingredient>> IngredientsSended => _ingredientsSended.GetItems();
    public CarState CarState => _carState;
    public float NowWaitTime => _nowWaitTime;

    public CarWaitManagerData(float defaultWaitTime) {
        _needWaitTime = defaultWaitTime;
        _carState = CarState.Calm;
        _ingredientsSended = new();
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

    public void SetIngredientsSended(IEnumerable<BuyableItemCount<Ingredient>> ingredients) {
        _ingredientsSended.Clear();

        foreach (var ingredient in ingredients)
            _ingredientsSended.Add(ingredient);
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
