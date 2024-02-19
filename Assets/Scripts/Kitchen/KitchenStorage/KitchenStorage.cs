using System;
using UnityEngine;

public class KitchenStorage : IngredientStorage
{
    [SerializeField] private Ingredient _lemon;
    public event Action IngredientsAdded;

    [ContextMenu("AddLemon")]
    public void AddLemon()
    {
        PutIngredient(new IngredientCount { Ingredient = _lemon, Count = 2});
        IngredientsAdded?.Invoke();
    }

    public void AddIngredients(IngredientCountList countList)
    {
        for (int i = 0; i < countList.Size; i++)
            PutIngredient(countList.Get(i));
        IngredientsAdded?.Invoke();
    }

    public void RemoveIngredients(IngredientCount[] countList)
    {
        for (int i = 0; i < countList.Length; i++)
            _ingredients.Remove(countList[i]);
        IngredientsAdded?.Invoke();
    }

    public void RemoveAll()
    {
        _ingredients.Clear();
    }
}
