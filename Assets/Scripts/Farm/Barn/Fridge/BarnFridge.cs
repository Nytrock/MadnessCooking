using System;
using UnityEngine;

public class BarnFridge : MonoBehaviour
{
    [SerializeField] private FarmCar _car;
    [SerializeField] private Ingredient _milk;
    [SerializeField] private Ingredient _flour;

    private int _milkCount;
    private int _flourCount;

    public event Action<int> MilkChanged;
    public event Action<int> FlourChanged;

    public void AddMilk(int count)
    {
        _milkCount += count;
        MilkChanged?.Invoke(_milkCount);
    }

    public void AddFlour(int count)
    {
        _flourCount += count;
        FlourChanged?.Invoke(_flourCount);
    }

    public void PutIngredient(Ingredient ingredient)
    {
        var carSpace = _car.GetSpace();
        if (carSpace == 0)
            return;

        if (ingredient == _milk) {
            FatigueManager.instance.ChangeFatigue(_milk.FatigueCount * _milkCount);
            MoveToCar(carSpace, ref _milkCount, _milk);
            MilkChanged?.Invoke(_milkCount);
        } else if (ingredient == _flour) {
            FatigueManager.instance.ChangeFatigue(_flour.FatigueCount * _flourCount);
            MoveToCar(carSpace, ref _flourCount, _flour);
            FlourChanged?.Invoke(_flourCount);
        } else {
            Debug.LogError("Unknown ingredient");
        }
    }

    private void MoveToCar(int carSpace, ref int count, Ingredient ingredient) {
        if (ingredient != _milk && ingredient != _flour) {
            Debug.LogError("Unknown ingredient");
            return;
        }

        if (carSpace < count) {
            _car.PutIngredient(new IngredientCount(ingredient, carSpace));
            count -= carSpace;
        } else {
            _car.PutIngredient(new IngredientCount(ingredient, count));
            count = 0;
        }
    }
}
