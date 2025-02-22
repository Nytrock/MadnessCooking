using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class CarWaitManagerData {
    [SerializeField, JsonProperty] private BuyableItemCountList<Ingredient> _ingredientsSended;
    [SerializeField, JsonProperty] private CarState _carState;
    [SerializeField, JsonProperty] private float _nowWaitTime;
    [SerializeField, JsonProperty] private float _needWaitTime;

    public IEnumerable<BuyableItemCount<Ingredient>> IngredientsSended => _ingredientsSended.GetItems();
    public CarState CarState => _carState;
    public float NowWaitTime => _nowWaitTime;

    public CarWaitManagerData(float defaultWaitTime) {
        _needWaitTime = defaultWaitTime;
        _carState = CarState.Calm;
        _ingredientsSended = new();
    }

    public void UpdateSpeed(float needTime) {
        _needWaitTime = needTime;
        if (_carState != CarState.Calm) {
            _nowWaitTime = Mathf.Min(_nowWaitTime, _needWaitTime / 2f);
        }
    }

    public void UpdateTime() {
        _nowWaitTime -= InGameTime.Instance.RawDeltaTime;
    }

    public void StartWait() {
        _nowWaitTime = _needWaitTime / 2f;
        _carState = CarState.Sent;
    }

    public void SetIngredientsSended(IEnumerable<BuyableItemCount<Ingredient>> ingredients) {
        _ingredientsSended.Clear();

        foreach (var ingredient in ingredients)
            _ingredientsSended.Add(new(ingredient));
    }

    public void StartReturn() {
        _carState = CarState.Returns;
        _ingredientsSended.Clear();
        _nowWaitTime = _needWaitTime / 2f;
    }

    public void StartCalm() {
        _carState = CarState.Calm;
    }
}
