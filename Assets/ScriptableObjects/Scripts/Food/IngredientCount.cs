using System;
using UnityEngine;

[Serializable]
public class IngredientCount
{
    [SerializeField] private Ingredient _ingredient;
    [SerializeField] private int _count;

    public Ingredient Ingredient => _ingredient;
    public int Count => _count;

    public IngredientCount(Ingredient ingredient, int count)
    {
        _ingredient = ingredient;
        _count = count;
    }

    public void ChangeCount(int count)
    {
        if (_count + count < 0)
            _count = 0;
        else
            _count += count;
    }
}
