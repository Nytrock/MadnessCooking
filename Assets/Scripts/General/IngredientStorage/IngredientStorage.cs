using System;
using UnityEngine;

public abstract class IngredientStorage<T> : MonoBehaviour, IBindable<T> where T: ISaveable
{
    [SerializeField] protected int _maxSize = 100;

    protected IngredientCountList _ingredients;
    protected T _data;
    protected int _nowSize = 0;

    public int LeftSpace => _maxSize - _nowSize;

    public event Action<IngredientCount> IngredientAdded;
    public event Action<int> MaxSizeChanged;

    protected void LateStart()
    {
        MaxSizeChanged?.Invoke(_maxSize);
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

        var addedElement = _ingredients.Add(newElement);
        if (addedElement == newElement)
            IngredientAdded?.Invoke(newElement);

        UpdateData();
    }

    public virtual void RemoveIngredients(IngredientCountList countList)
    {
        for (int i = 0; i < countList.Size; i++)
            _ingredients.Remove(countList.Get(i));
        UpdateData();
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

    protected void InvokeSizeChange()
    {
        MaxSizeChanged?.Invoke(_maxSize);
    }

    public abstract void Bind(T data, bool isFileEmpty);
    protected abstract void UpdateData();
}
