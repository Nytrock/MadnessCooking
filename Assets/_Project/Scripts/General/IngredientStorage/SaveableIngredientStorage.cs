using UnityEngine;

public abstract class SaveableIngredientStorage<TData> : IngredientStorage, IBindable<TData>
    where TData : ISaveable {

    [SerializeField] protected BuyableItemCountList<Ingredient> _defaultIngredients;

    public virtual void Bind(TData data) {
        foreach (var ingredientCount in Data.Ingredients)
            InvokeIngredientCountAdded(ingredientCount);
    }
}
