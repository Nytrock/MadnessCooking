using UnityEngine;

public abstract class SaveableIngredientStorage<TData> : IngredientStorage, IBindable<TData>
    where TData : ISaveable {

    [SerializeField] protected IngredientCountList _defaultIngredients;

    public void LateStart() {
        Data.SetMaxSpace(_defaultMaxSpace);
        foreach (var ingredientCount in Data.Ingredients)
            InvokeIngredientAdded(ingredientCount);
    }

    public abstract void Bind(TData data);
}
