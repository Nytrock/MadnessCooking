using System;
using UnityEngine;

public class IngredientStorage : MonoBehaviour
{
    [SerializeField] private int _maxSize = 100;
    protected IngredientCountList _ingredients = new();
    protected int _nowSize = 0;

    public int MaxSize => _maxSize;

    public event Action<int> CountChanged;

    public int GetSpace()
    {
        return _maxSize - _nowSize;
    }

    public void PutIngredient(int count, Ingredient plantedIngredient)
    {
        _nowSize += count;
        var newElement = new IngredientCount
        {
            Count = count,
            Ingredient = plantedIngredient
        };
        _ingredients.Add(newElement);

        var index = _ingredients.IndexOf(newElement);
        CountChanged?.Invoke(index);
    }

    public void PutIngredient(IngredientCount newElement)
    {
        if (_maxSize != -1)
            _nowSize += newElement.Count;
        _ingredients.Add(newElement);

        var index = _ingredients.IndexOf(newElement);
        CountChanged?.Invoke(index);
    }

    public IngredientCount GetIngredient(int index)
    {
        return _ingredients.Get(index);
    }

    public IngredientCountList GetList()
    {
        return _ingredients.Copy();
    }
}
