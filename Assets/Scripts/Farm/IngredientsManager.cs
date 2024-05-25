using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IngredientsManager : Singleton<IngredientsManager>, IBindable<FarmData>
{
    [SerializeField] private Ingredient[] _allIngredients;
    [SerializeField] private Ingredient[] _defaultIngredients;

    [Header("Static ingredients")]
    [SerializeField] private Ingredient _spice;
    [SerializeField] private Ingredient _milk;
    [SerializeField] private Ingredient _flour;
    [SerializeField] private Ingredient _egg;
    [SerializeField] private Ingredient _wheat;

    public Ingredient Spice => _spice;
    public Ingredient Milk => _milk;
    public Ingredient Flour => _flour;
    public Ingredient Egg => _egg;
    public Ingredient Wheat => _wheat;

    private FarmData _data;

    public bool HaveIngredient(Ingredient ingredient)
    {
        return _data.AvailableIngredients.Contains(ingredient);
    }

    public bool HaveIngredientsOfBedType(BedType bedType)
    {
        foreach (var ingredient in _data.AvailableIngredients)
            if (ingredient.Type == bedType.AcceptableType)
                return true;
        return false;
    }

    public IEnumerable<Ingredient> GetAvailableIngredientsOfBedType(BedType bedType)
    {
        foreach (var ingredient in _data.AvailableIngredients)
            if (ingredient.Type == bedType.AcceptableType)
                yield return ingredient;
        yield break;
    }

    public IEnumerable<Ingredient> GetAllIngredientsOfBedType(BedType bedType)
    {
        foreach (var ingredient in _allIngredients)
            if (ingredient.Type == bedType.AcceptableType)
                yield return ingredient;
        yield break;
    }

    public void AddIngredient(Ingredient ingredient)
    {
        _data.AvailableIngredients.Add(ingredient);
    }

    public void Bind(FarmData data, bool isFileEmpty)
    {
        _data = data;
        if (isFileEmpty)
            _data.AvailableIngredients = _defaultIngredients.ToList();
    }
}
