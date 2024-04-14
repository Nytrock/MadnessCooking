using System.Collections.Generic;
using UnityEngine;

public class IngredientsManager : MonoBehaviour
{
    [SerializeField] private Ingredient[] _allIngredients;
    [SerializeField] private List<Ingredient> _haveIngredients;

    public List<Ingredient> HaveIngredients => _haveIngredients;

    public bool HaveIngredient(Ingredient ingredient)
    {
        return _haveIngredients.Contains(ingredient);
    }

    public List<Ingredient> HaveIngredientsOfBedType(BedType bedType)
    {
        var result = new List<Ingredient>();
        foreach (var ingredient in _haveIngredients)
            if (ingredient.Type == bedType.AcceptableType)
                result.Add(ingredient);
        return result;
    }

    public List<Ingredient> GetIngredientsOfBedType(BedType bedType)
    {
        var result = new List<Ingredient>();
        foreach (var ingredient in _allIngredients)
            if (ingredient.Type == bedType.AcceptableType)
                result.Add(ingredient);
        return result;
    }

    public void AddIngredient(Ingredient ingredient)
    {
        _haveIngredients.Add(ingredient);
    }
}
