using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class IngredientStorage<TData> : MonoBehaviour, IBindable<TData>
    where TData : ISaveable {

    [SerializeField] protected int _defaultMaxSpace = 100;

    public IngredientStorageData Data { get; protected set; }

    public event Action<BuyableItemCount<Ingredient>> IngredientAdded;

    public void PutIngredients(IEnumerable<BuyableItemCount<Ingredient>> puttingCountList) {
        foreach (var count in puttingCountList)
            PutIngredientWithRemain(count.Item, count.Count);
    }

    public virtual int PutIngredientWithRemain(Ingredient ingredient, int count) {
        int remainCount = 0;
        BuyableItemCount<Ingredient> puttingCount = new(ingredient, count);

        if (!Data.CanAddCount(count)) {
            remainCount = Data.NowSpace + count - Data.MaxSpace;
            puttingCount = new(ingredient, Data.LeftSpace);
        }

        int oldSize = Data.Ingredients.Size;
        Data.Ingredients.Add(puttingCount);
        Data.AddCount(puttingCount.Count);
        if (Data.Ingredients.Size != oldSize)
            IngredientAdded?.Invoke(puttingCount);
        return remainCount;
    }

    public virtual void RemoveIngredients(IEnumerable<BuyableItemCount<Ingredient>> ingredients) {
        foreach (var count in ingredients)
            Data.Ingredients.Remove(count);
    }

    public bool HaveCount(BuyableItemCount<Ingredient> count) {
        return Data.Ingredients.ContainsCount(count);
    }

    public virtual void Bind(TData data, bool isFileEmpty) {
        foreach (var ingredientCount in Data.Ingredients)
            IngredientAdded?.Invoke(ingredientCount);
    }
}
