using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IngredientsManager : MonoBehaviour, IBindable<FarmData> {
    [SerializeField] private Ingredient[] _allIngredients;
    [SerializeField] private Ingredient[] _defaultIngredients;
    private FarmData _data;

    public bool HaveIngredient(Ingredient ingredient) {
        return _data.AvailableIngredients.Contains(ingredient);
    }

    public bool HaveIngredientsOfBedType(BedType bedType) {
        foreach (var ingredient in _data.AvailableIngredients)
            if (ingredient.Type == bedType.AcceptableType)
                return true;
        return false;
    }

    public IEnumerable<Ingredient> GetAvailableIngredientsOfBedType(BedType bedType) {
        foreach (var ingredient in _data.AvailableIngredients)
            if (ingredient.Type == bedType.AcceptableType)
                yield return ingredient;
    }

    public IEnumerable<Ingredient> GetAllIngredientsOfBedType(BedType bedType) {
        foreach (var ingredient in _allIngredients)
            if (ingredient.Type == bedType.AcceptableType)
                yield return ingredient;
    }

    public void AddIngredient(Ingredient ingredient) {
        _data.AvailableIngredients.Add(ingredient);
    }

    public void Bind(FarmData data, bool isFileEmpty) {
        _data = data;
        if (isFileEmpty)
            _data.AvailableIngredients = _defaultIngredients.ToList();
    }
}
