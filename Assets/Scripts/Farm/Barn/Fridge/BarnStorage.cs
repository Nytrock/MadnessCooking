using System;
using UnityEngine;

public class BarnStorage : IngredientStorage {
    [SerializeField] private FarmCar _car;
    [SerializeField] private Cow _cow;
    [SerializeField] private FlourMill _flourMill;

    private Ingredient _milk;
    private Ingredient _flour;
    private int _milkCount = 0;

    public event Action<int, int> CountsUpdated;

    private void Awake() {
        Data = new(_defaultMaxSpace);
        _cow.CountChanged += UpdateCounts;
        _flourMill.CountChanged += UpdateCounts;
    }

    private void Start() {
        _milk = ConstIngredients.Instance.Milk;
        _flour = ConstIngredients.Instance.Flour;
        UpdateMilkCount();
    }

    private void UpdateCounts() {
        UpdateMilkCount();
        CountsUpdated?.Invoke(_cow.Data.ReadyCount, _flourMill.Data.ReadyCount);
    }

    private void UpdateMilkCount() {
        int difference = _cow.Data.ReadyCount - _milkCount;
        if (difference > 0)
            PutIngredientWithRemain(_milk, difference);
        else if (difference < 0)
            RemoveIngredient(_milk, -difference);

        _milkCount = _cow.Data.ReadyCount;
    }

    public void PutIngredient(Ingredient ingredient) {
        if (ingredient != _milk && ingredient != _flour)
            throw new ArgumentException("Unknown ingredient");

        if (_car.Data.LeftSpace == 0)
            return;

        MoveToCar(ingredient);
        UpdateCounts();
    }

    private void MoveToCar(Ingredient ingredient) {
        NeedHoldAdd changingHoldAdd;
        if (ingredient == _milk)
            changingHoldAdd = _cow;
        else
            changingHoldAdd = _flourMill;

        int oldCount = changingHoldAdd.Data.ReadyCount;
        int remainCount = _car.PutIngredientWithRemain(ingredient, changingHoldAdd.Data.ReadyCount);
        FatigueManager.Instance.ChangeFatigue(ingredient.FatigueCoef * (changingHoldAdd.Data.ReadyCount - remainCount));
        changingHoldAdd.SetReady(remainCount);

        if (ingredient == _milk)
            RemoveIngredient(ingredient, oldCount - remainCount);
    }
}
