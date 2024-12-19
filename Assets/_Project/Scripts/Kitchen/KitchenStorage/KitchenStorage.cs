public class KitchenStorage : SaveableIngredientStorage<KitchenData> {
    public void RemoveAll() {
        Data.ClearList();
    }

    public override void Bind(KitchenData data) {
        data.KitchenStorage ??= new(_defaultMaxSpace, _defaultIngredients);
        Data = data.KitchenStorage;
    }
}
