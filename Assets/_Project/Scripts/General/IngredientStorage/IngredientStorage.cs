using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class IngredientStorage : MonoBehaviour {
    [SerializeField] protected int _defaultMaxSpace = 100;

    [field: SerializeField] public IngredientStorageData Data { get; protected set; }

    public event Action<BuyableItemCount<Ingredient>> IngredientAdded;
    public event Action<Ingredient> IngredientRemoved;

    public event Action<BuyableItemCount<Ingredient>> IngredientCountAdded;
    public event Action<BuyableItemCount<Ingredient>> IngredientCountRemoved;

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

        if (!Data.ContainsIngredient(puttingCount.Item))
            InvokeIngredientAdded(puttingCount);
        else
            IngredientCountAdded?.Invoke(puttingCount);

        Data.AddIngredient(puttingCount);
        Data.AddCount(puttingCount.Count);
        return remainCount;
    }

    public virtual void RemoveIngredients(IEnumerable<BuyableItemCount<Ingredient>> ingredients) {
        foreach (var count in ingredients)
            RemoveIngredient(count);
    }

    public void RemoveIngredient(Ingredient ingredient, int count) {
        BuyableItemCount<Ingredient> removingCount = new(ingredient, count);
        RemoveIngredient(removingCount);
    }

    public void RemoveIngredient(BuyableItemCount<Ingredient> removingCount) {
        Data.RemoveIngredient(removingCount);
        IngredientCountRemoved?.Invoke(removingCount);

        if (Data.GetIngredientCount(removingCount.Item) == 0)
            IngredientRemoved?.Invoke(removingCount.Item);
    }

    public bool HaveCount(BuyableItemCount<Ingredient> count) {
        return Data.ContainsCount(count);
    }

    protected void InvokeIngredientAdded(BuyableItemCount<Ingredient> count) {
        IngredientAdded?.Invoke(count);
        IngredientCountAdded?.Invoke(count);
    }
}
