using System;
using UnityEngine;

public abstract class IngredientStorage<T> : MonoBehaviour, IBindable<T> where T: ISaveable
{
    [SerializeField] protected int _defaultMaxSpace = 100;

    public SerializableIngredientStorage Data { get; protected set; }

    public event Action<IngredientCount> IngredientAdded;

    public virtual void PutIngredients(IngredientCountList newElementsList)
    {
        for (int i = 0; i < newElementsList.Size; i++)
            PutIngredient(newElementsList.Get(i));
    }

    public virtual void PutIngredient(IngredientCount newElement)
    {
        if (!Data.TryAddCount(newElement.Count))
            Debug.LogError("Too big count");

        var oldSize = Data.Ingredients.Size;
        Data.Ingredients.Add(newElement);
        if (Data.Ingredients.Size != oldSize)
            IngredientAdded?.Invoke(newElement);
    }

    public virtual void RemoveIngredients(IngredientCountList countList)
    {
        for (int i = 0; i < countList.Size; i++)
            Data.Ingredients.Remove(countList.Get(i));
    }

    public bool HaveCount(IngredientCount count)
    {
        return Data.Ingredients.ContainsCount(count);
    }

    public virtual void Bind(T data, bool isFileEmpty)
    {
        foreach (var item in Data.Ingredients)
            IngredientAdded?.Invoke(item);
    }
}
