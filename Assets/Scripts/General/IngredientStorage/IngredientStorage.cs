using System;
using UnityEngine;

public abstract class IngredientStorage<TData> : MonoBehaviour, IBindable<TData> where TData: ISaveable
{
    [SerializeField] protected int _defaultMaxSpace = 100;

    public SerializableIngredientStorage Data { get; protected set; }

    public event Action<IngredientCount> IngredientAdded;

    public virtual void PutIngredients(IngredientCountList puttingCountList)
    {
        for (int i = 0; i < puttingCountList.Size; i++)
            PutIngredient(puttingCountList.Get(i));
    }

    public virtual void PutIngredient(IngredientCount puttingCount)
    {
        if (!Data.TryAddCount(puttingCount.Count))
            throw new OverflowException("Too big count");

        int oldSize = Data.Ingredients.Size;
        Data.Ingredients.Add(puttingCount);
        if (Data.Ingredients.Size != oldSize)
            IngredientAdded?.Invoke(puttingCount);
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

    public virtual void Bind(TData data, bool isFileEmpty)
    {
        foreach (var ingredientCount in Data.Ingredients)
            IngredientAdded?.Invoke(ingredientCount);
    }
}
