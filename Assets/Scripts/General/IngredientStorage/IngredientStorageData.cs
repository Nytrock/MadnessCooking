using System;
using UnityEngine;

[Serializable]
public class IngredientStorageData {
    [SerializeField] private BuyableItemCountList<Ingredient> _ingredients;
    [SerializeField] private int _maxSpace;
    [SerializeField] private int _nowSpace;

    public BuyableItemCountList<Ingredient> Ingredients => _ingredients;
    public int NowSpace => _nowSpace;
    public int MaxSpace => _maxSpace;
    public int LeftSpace => _maxSpace - _nowSpace;

    public IngredientStorageData(int maxSize) {
        _ingredients = new();
        _maxSpace = maxSize;
        _nowSpace = 0;
    }

    public bool CanAddCount(int count) {
        return _nowSpace + count <= _maxSpace || _maxSpace == -1;
    }

    public void AddCount(int count) {
        if (!CanAddCount(count)) {
            _nowSpace = _maxSpace;
            return;
        }

        _nowSpace += count;
    }

    public void ClearList() {
        _ingredients.Clear();
        _nowSpace = 0;
    }

    public void UpdateMaxSpace(CountUpgrade upgrade) {
        _maxSpace = upgrade.Count;
    }
}
