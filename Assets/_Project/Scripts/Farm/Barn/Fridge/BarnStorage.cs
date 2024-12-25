using System;
using UnityEngine;

public class BarnStorage : IngredientStorage {
    [SerializeField] private FarmCar _car;
    [SerializeField] private Cow _cow;
    [SerializeField] private FlourMill _flourMill;

    private Ingredient _milk;
    private Ingredient _flour;
    private int _milkCount = 0;

    public event Action<int> MilkCountUpdated;
    public event Action<int> FlourCountUpdated;

    private void Awake() {
        Data = new(_defaultMaxSpace);

        _cow.ReadyCountChanged += UpdateMilkCount;
        _flourMill.ReadyCountChanged += UpdateFlourCount;
    }

    private void Start() {
        _milk = ConstIngredients.Instance.Milk;
        _flour = ConstIngredients.Instance.Flour;
    }

    private void UpdateMilkCount() {
        int difference = _cow.Data.ReadyCount - _milkCount;

        if (difference > 0)
            PutIngredientWithRemain(_milk, difference);
        else if (difference < 0)
            RemoveIngredient(_milk, -difference);

        _milkCount = _cow.Data.ReadyCount;
        MilkCountUpdated?.Invoke(_cow.Data.ReadyCount);
    }

    private void UpdateFlourCount() {
        FlourCountUpdated?.Invoke(_flourMill.Data.ReadyCount);
    }

    public void PutIngredient(Ingredient ingredient) {
        if (ingredient != _milk && ingredient != _flour)
            throw new ArgumentException("Unknown ingredient");

        if (_car.Data.LeftSpace == 0)
            return;

        MoveToCar(ingredient);
        UpdateFlourCount();
    }

    private void MoveToCar(Ingredient ingredient) {
        NeedHoldAdd changingHoldAdd;
        if (ingredient == _milk)
            changingHoldAdd = _cow;
        else
            changingHoldAdd = _flourMill;

        int oldCount = changingHoldAdd.Data.ReadyCount;
        if (oldCount == 0)
            return;

        int remainCount = _car.PutIngredientWithRemain(ingredient, changingHoldAdd.Data.ReadyCount);
        FatigueManager.Instance.ChangeFatigue(ingredient.FatigueCoef * (changingHoldAdd.Data.ReadyCount - remainCount));
        changingHoldAdd.SetReady(remainCount);

        if (ingredient == _milk)
            RemoveIngredient(ingredient, oldCount - remainCount);
    }
}
