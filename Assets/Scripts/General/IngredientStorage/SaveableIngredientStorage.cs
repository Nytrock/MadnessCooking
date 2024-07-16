public abstract class SaveableIngredientStorage<TData> : IngredientStorage, IBindable<TData>
    where TData : ISaveable {

    public virtual void Bind(TData data, bool isFileEmpty) {
        foreach (var ingredientCount in Data.Ingredients)
            InvokeIngredientCountAdded(ingredientCount);
    }
}
