using System;
using UnityEngine;

public class BarnStorage : MonoBehaviour {
    [SerializeField] private FarmCar _car;
    [SerializeField] private Cow _cow;
    [SerializeField] private FlourMill _flourMill;
    private Ingredient _milk;
    private Ingredient _flour;

    public event Action<int, int> CountsUpdated;

    private void Start() {
        _milk = ConstIngredients.Instance.Milk;
        _flour = ConstIngredients.Instance.Flour;
    }

    public void PutIngredient(Ingredient ingredient) {
        if (ingredient != _milk && ingredient != _flour)
            throw new ArgumentException("Unknown ingredient");

        if (_car.Data.LeftSpace == 0)
            return;

        MoveToCar(ingredient);
        CountsUpdated?.Invoke(_cow.ReadyCount, _flourMill.ReadyCount);
    }

    private void MoveToCar(Ingredient ingredient) {
        NeedHoldAdd changingHoldAdd;
        if (ingredient == _milk)
            changingHoldAdd = _cow;
        else
            changingHoldAdd = _flourMill;

        int remainCount = _car.PutIngredientWithRemain(ingredient, changingHoldAdd.ReadyCount);
        FatigueManager.Instance.ChangeFatigue(ingredient.FatigueCoef * (changingHoldAdd.ReadyCount - remainCount));
        changingHoldAdd.SetReady(remainCount);
    }
}
