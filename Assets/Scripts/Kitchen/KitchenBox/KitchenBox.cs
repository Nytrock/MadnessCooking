using System;
using UnityEngine;

public class KitchenBox : IngredientStorage
{
    public event Action IngredientsAdded;

    public void AddIngrediens(IngredientCountList countList)
    {
        for (int i = 0; i < countList.Size; i++)
            PutIngredient(countList.Get(i));
        IngredientsAdded?.Invoke();
    }
}
