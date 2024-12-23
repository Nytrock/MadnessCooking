using UnityEngine;

public abstract class SaveableIngredientStorage<TData> : IngredientStorage, IBindable<TData>
    where TData : ISaveable {

    [SerializeField] protected BuyableItemCountList<Ingredient> _defaultIngredients;

    public void LateStart() {
        foreach (var ingredientCount in Data.Ingredients)
            InvokeIngredientAdded(ingredientCount);
    }

    public abstract void Bind(TData data);
}
