public class KitchenStorage : SaveableIngredientStorage<KitchenData> {
    public override void Bind(KitchenData data) {
        data.KitchenStorage ??= new(_defaultIngredients);
        Data = data.KitchenStorage;
    }

    public void RemoveAllSpices() {
        int spiceCount = Data.GetIngredientCount(ConstIngredients.Instance.Spice);
        RemoveIngredient(ConstIngredients.Instance.Spice, spiceCount);
    }
}
