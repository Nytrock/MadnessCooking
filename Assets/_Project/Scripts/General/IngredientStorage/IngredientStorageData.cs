using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class IngredientStorageData {
    [SerializeField, JsonProperty] private BuyableItemCountList<Ingredient> _ingredients;
    [SerializeField, JsonProperty] private int _maxSpace;
    [SerializeField, JsonProperty] private int _nowSpace;

    public IEnumerable<BuyableItemCount<Ingredient>> Ingredients => _ingredients.GetItems();
    public int NowSpace => _nowSpace;
    public int MaxSpace => _maxSpace;
    public int LeftSpace => _maxSpace - _nowSpace;

    public IngredientStorageData(BuyableItemCountList<Ingredient> defaultIngredients = null) {
        if (defaultIngredients == null)
            _ingredients = new();
        else
            _ingredients = new(defaultIngredients);
        _nowSpace = 0;
    }

    public bool CanAddCount(int count) {
        return _nowSpace + count <= _maxSpace || _maxSpace == -1;
    }

    public void ClearList() {
        _ingredients.Clear();
        _nowSpace = 0;
    }

    public void UpdateMaxSpace(CountUpgrade upgrade) {
        _maxSpace = upgrade.Count;
    }

    public void AddIngredientCount(BuyableItemCount<Ingredient> puttingCount) {
        _ingredients.Add(puttingCount);

        if (CanAddCount(puttingCount.Count))
            _nowSpace += puttingCount.Count;
        else
            _nowSpace = _maxSpace;
    }

    public bool ContainsCount(BuyableItemCount<Ingredient> count) {
        return _ingredients.ContainsCount(count);
    }

    public void RemoveIngredient(BuyableItemCount<Ingredient> count) {
        _ingredients.Remove(count);
    }

    public int GetIngredientCount(Ingredient ingredient) {
        return _ingredients.GetItemCount(ingredient);
    }

    public bool ContainsIngredient(Ingredient item) {
        return _ingredients.ContainsItem(item);
    }

    public void SetMaxSpace(int defaultMaxSpace) {
        _maxSpace = defaultMaxSpace;
    }
}
