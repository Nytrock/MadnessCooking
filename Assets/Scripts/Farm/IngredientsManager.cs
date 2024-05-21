using System.Collections.Generic;
using UnityEngine;

public class IngredientsManager : MonoBehaviour, IBindable<FarmData>
{
    [SerializeField] private Ingredient[] _allIngredients;
    [SerializeField] private List<Ingredient> _defaultIngredients;
    private FarmData _data;

    public bool HaveIngredient(Ingredient ingredient)
    {
        return _data.AvailableIngredients.Contains(ingredient);
    }

    public List<Ingredient> HaveIngredientsOfBedType(BedType bedType)
    {
        var result = new List<Ingredient>();
        foreach (var ingredient in _data.AvailableIngredients)
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
        _data.AvailableIngredients.Add(ingredient);
    }

    public void Bind(FarmData data, bool isFileEmpty)
    {
        _data = data;
        if (isFileEmpty)
            _data.AvailableIngredients = _defaultIngredients;
    }
}
