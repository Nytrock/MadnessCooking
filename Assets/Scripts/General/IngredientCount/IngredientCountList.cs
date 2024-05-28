using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class IngredientCountList {
    [SerializeField] private List<IngredientCount> _ingredientCounts = new();
    private List<Ingredient> _availableIngredients = new();

    public int Size => _ingredientCounts.Count;

    public void Add(IngredientCount ingredientCount) {
        if (ContainsIngredient(ingredientCount))
            _ingredientCounts[IndexOf(ingredientCount)].ChangeCount(ingredientCount.Count);
        else
            _ingredientCounts.Add(ingredientCount);

        UpdateAvailableIngredients();
    }

    public void Extend(IngredientCountList ingredientCountList) {
        foreach (var ingredientCount in ingredientCountList)
            Add(ingredientCount);
    }

    public void Remove(IngredientCount ingredientCount) {
        if (!ContainsIngredient(ingredientCount))
            return;

        int index = IndexOf(ingredientCount);
        _ingredientCounts[index].ChangeCount(-ingredientCount.Count);
        if (_ingredientCounts[index].Count == 0)
            _ingredientCounts.RemoveAt(index);

        UpdateAvailableIngredients();
    }

    private void UpdateAvailableIngredients() {
        _availableIngredients = _ingredientCounts.Select(x => x.Ingredient).ToList();
    }

    public bool ContainsIngredient(IngredientCount ingredientCount) {
        return _availableIngredients.Contains(ingredientCount.Ingredient);
    }

    public bool ContainsCount(IngredientCount ingredientCount) {
        if (!ContainsIngredient(ingredientCount))
            return false;
        return _ingredientCounts[IndexOf(ingredientCount)].Count >= ingredientCount.Count;
    }

    public int IndexOf(IngredientCount ingredientCount) {
        return _availableIngredients.IndexOf(ingredientCount.Ingredient);
    }

    public IngredientCount Get(int index) {
        return _ingredientCounts[index];
    }

    public void Clear() {
        _ingredientCounts.Clear();
        _availableIngredients.Clear();
    }

    public IEnumerator<IngredientCount> GetEnumerator() {
        foreach (var count in _ingredientCounts)
            yield return count;
        yield break;
    }
}
