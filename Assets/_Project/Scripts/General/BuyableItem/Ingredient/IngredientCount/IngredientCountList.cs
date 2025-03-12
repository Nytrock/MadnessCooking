using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class IngredientCountList {
    [SerializeField, JsonProperty] private List<IngredientCount> _itemCounts;
    private List<Ingredient> _availableIngredients;

    public IngredientCountList() {
        _itemCounts = new();
        _availableIngredients = new();
    }

    public IngredientCountList(IngredientCountList ingredientsList) : this() {
        foreach (var item in ingredientsList)
            _itemCounts.Add(new(item));
        UpdateAvailableIngredients();
    }

    public void Add(IngredientCount newIngredientCount) {
        foreach (var itemCount in _itemCounts) {
            if (itemCount.Item == newIngredientCount.Item) {
                itemCount.AddToCount(newIngredientCount.Count);
                UpdateAvailableIngredients();
                return;
            }
        }

        _itemCounts.Add(newIngredientCount);
        UpdateAvailableIngredients();
    }

    public void Remove(IngredientCount removingIngredientCount) {
        for (int i = 0; i < _itemCounts.Count; i++) {
            if (_itemCounts[i].Item == removingIngredientCount.Item) {
                _itemCounts[i].AddToCount(-removingIngredientCount.Count);
                if (_itemCounts[i].Count <= 0)
                    _itemCounts.RemoveAt(i);
                break;
            }
        }

        UpdateAvailableIngredients();
    }

    public void Remove(Ingredient ingredient, int count) {
        Remove(new(ingredient, count));
    }

    private void UpdateAvailableIngredients() {
        _availableIngredients = _itemCounts.Select(count => count.Item).ToList();
    }

    public bool ContainsIngredient(Ingredient ingredient) {
        UpdateAvailableIngredients();
        return _availableIngredients.Contains(ingredient);
    }

    public bool ContainsIngredientCount(IngredientCount searchingCount) {
        foreach (var countIngredient in _itemCounts) {
            if (countIngredient.Item == searchingCount.Item) {
                if (countIngredient.Count >= searchingCount.Count)
                    return true;
                else
                    return false;
            }
        }

        return false;
    }

    public int IndexOf(IngredientCount ingredientCount) {
        return _availableIngredients.IndexOf(ingredientCount.Item);
    }

    public void Clear() {
        _itemCounts.Clear();
        _availableIngredients.Clear();
    }

    public IEnumerator<IngredientCount> GetEnumerator() {
        foreach (var count in _itemCounts)
            yield return count;
    }

    public IEnumerable<IngredientCount> GetItems() {
        foreach (var count in _itemCounts)
            yield return count;
    }

    public int GetItemCount(Ingredient ingredient) {
        foreach (var count in _itemCounts)
            if (count.Item == ingredient)
                return count.Count;
        return 0;
    }
}
