using System;
using UnityEngine;

public class BarnFridge : MonoBehaviour
{
    [SerializeField] private Car _car;

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

        if (ingredient.name == "Milk") {
            MoveToCar(carSpace, ref _milkCount, ingredient);
            MilkChanged?.Invoke(_milkCount);
        } else {
            MoveToCar(carSpace, ref _flourCount, ingredient);
            FlourChanged?.Invoke(_flourCount);
        }
    }

    private void MoveToCar(int carSpace, ref int count, Ingredient ingredient) {
        if (carSpace < count) {
            _car.PutIngredient(new IngredientCount(ingredient, carSpace));
            count -= carSpace;
        } else {
            _car.PutIngredient(new IngredientCount(ingredient, count));
            count = 0;
        }
    }
}
