using System;
using UnityEngine;

public class IngredientStorage : MonoBehaviour
{
    [SerializeField] private int _maxSize = 100;
    protected IngredientCountList _ingredients = new();
    protected int _nowSize = 0;

    public int MaxSize => _maxSize;

    public event Action<int> ElementCountChanged;

    public int GetSpace()
    {
        return _maxSize - _nowSize;
    }

    public virtual void PutIngredients(IngredientCountList newElementsList)
    {
        for (int i = 0; i < newElementsList.Size; i++)
            PutIngredient(newElementsList.Get(i));
    }

    public virtual void PutIngredient(IngredientCount newElement)
    {
        if (_maxSize != -1)
            _nowSize += newElement.Count;
        _ingredients.Add(newElement);

        var index = _ingredients.IndexOf(newElement);
        ElementCountChanged?.Invoke(index);
    }

    public virtual void RemoveIngredients(IngredientCount[] countList)
    {
        for (int i = 0; i < countList.Length; i++)
            _ingredients.Remove(countList[i]);
    }

    public IngredientCount GetIngredientByIndex(int index)
    {
        return _ingredients.Get(index);
    }

    public IngredientCountList GetList()
    {
        return _ingredients.Copy();
    }

    public bool HaveCount(IngredientCount count)
    {
        return _ingredients.ContainsCount(count);
    }
}
