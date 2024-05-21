using System;
using UnityEngine;

[Serializable]
public class SerializableIngredientStorage
{
    [SerializeField] private IngredientCountList _ingredients;
    [SerializeField] private int _maxSpace;
    [SerializeField] private int _nowSpace = 0;

    public IngredientCountList Ingredients => _ingredients;
    public int NowSpace => _nowSpace;
    public int MaxSpace => _maxSpace;
    public int LeftSpace => _maxSpace - _nowSpace;

    public SerializableIngredientStorage(int maxSize)
    {
        _ingredients = new();
        _maxSpace = maxSize;
    }

    public bool TryAddCount(int count)
    {
        if (_nowSpace + count > _maxSpace && _maxSpace != -1)
            return false;

        _nowSpace += count;
        return true;
    }

    public void ClearList()
    {
        _ingredients.Clear();
        _nowSpace = 0;
    }

    public void UpdateMaxSpace(CountUpgrade upgrade)
    {
        _maxSpace = upgrade.Count;
    }
}
