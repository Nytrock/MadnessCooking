using System;
using UnityEngine;

public class KitchenStorage : IngredientStorage
{
    [SerializeField] private Ingredient _lemon;
    public event Action IngredientsChanged;

    [ContextMenu("AddLemon")]
    public void AddLemon()
    {
        PutIngredient(new IngredientCount(_lemon, 2));
        IngredientsChanged?.Invoke();
    }

    public override void PutIngredients(IngredientCountList newElementsList)
    {
        base.PutIngredients(newElementsList);
        IngredientsChanged?.Invoke();
    }

    public override void PutIngredient(IngredientCount newElement)
    {
        base.PutIngredient(newElement);
        IngredientsChanged?.Invoke();
    }

    public override void RemoveIngredients(IngredientCountList countList)
    {
        base.RemoveIngredients(countList);
        IngredientsChanged?.Invoke();
    }

    public void RemoveAll()
    {
        _ingredients.Clear();
        IngredientsChanged?.Invoke();
    }
}
