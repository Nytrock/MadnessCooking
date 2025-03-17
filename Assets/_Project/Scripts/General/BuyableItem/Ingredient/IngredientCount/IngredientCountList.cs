using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class IngredientCountList {
    [SerializeField, JsonProperty] private List<IngredientCount> _itemCounts;

    public IngredientCountList() {
        _itemCounts = new();
    }

    public IngredientCountList(IngredientCountList ingredientsList) : this() {
        foreach (var item in ingredientsList)
            _itemCounts.Add(new(item));
    }

    public void Add(IngredientCount newIngredientCount) {
        foreach (var itemCount in _itemCounts) {
            if (itemCount.Ingredient == newIngredientCount.Ingredient) {
                itemCount.AddToCount(newIngredientCount.Count);
                return;
            }
        }

        _itemCounts.Add(newIngredientCount);
    }

    public void Remove(IngredientCount removingIngredientCount) {
        for (int i = 0; i < _itemCounts.Count; i++) {
            if (_itemCounts[i].Ingredient == removingIngredientCount.Ingredient) {
                _itemCounts[i].AddToCount(-removingIngredientCount.Count);
                if (_itemCounts[i].Count <= 0)
                    _itemCounts.RemoveAt(i);
                break;
            }
        }
    }

    public void Remove(Ingredient ingredient, int count) {
        Remove(new(ingredient, count));
    }

    public bool ContainsIngredient(Ingredient ingredient) {
        foreach (var ingredientCount in _itemCounts)
            if (ingredientCount.Ingredient == ingredient)
                return true;
        return false;
    }

    public bool ContainsIngredientCount(IngredientCount searchingCount) {
        foreach (var countIngredient in _itemCounts) {
            if (countIngredient.Ingredient == searchingCount.Ingredient) {
                if (countIngredient.Count >= searchingCount.Count)
                    return true;
                else
                    return false;
            }
        }

        return false;
    }

    public void Clear() {
        _itemCounts.Clear();
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
            if (count.Ingredient == ingredient)
                return count.Count;
        return 0;
    }
}
