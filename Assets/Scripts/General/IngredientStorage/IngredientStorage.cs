using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class IngredientStorage<TData> : MonoBehaviour, IBindable<TData>
    where TData : ISaveable {

    [SerializeField] protected int _defaultMaxSpace = 100;

    public IngredientStorageData Data { get; protected set; }

    public event Action<IngredientCount> IngredientAdded;

    public virtual void PutIngredients(IngredientCountList puttingCountList) {
        for (int i = 0; i < puttingCountList.Size; i++)
            PutIngredientWithRemain(puttingCountList.Get(i));
    }

    public virtual int PutIngredientWithRemain(IngredientCount puttingCount) {
        int remainCount = 0;
        if (!Data.CanAddCount(puttingCount.Count)) {
            remainCount = Data.NowSpace + puttingCount.Count - Data.MaxSpace;
            puttingCount = new(puttingCount.Ingredient, Data.LeftSpace);
        }

        int oldSize = Data.Ingredients.Size;
        Data.Ingredients.Add(puttingCount);
        Data.AddCount(puttingCount.Count);
        if (Data.Ingredients.Size != oldSize)
            IngredientAdded?.Invoke(puttingCount);
        return remainCount;
    }

    public virtual void RemoveIngredients(IEnumerable<IngredientCount> ingredients) {
        foreach (var count in ingredients)
            Data.Ingredients.Remove(count);
    }

    public bool HaveCount(IngredientCount count) {
        return Data.Ingredients.ContainsCount(count);
    }

    public virtual void Bind(TData data, bool isFileEmpty) {
        foreach (var ingredientCount in Data.Ingredients)
            IngredientAdded?.Invoke(ingredientCount);
    }
}
