using UnityEngine;

public class KitchenStorage : SaveableIngredientStorage<KitchenData> {

    [ContextMenu("AddLemon")]
    public void AddLemon() {
        PutIngredientWithRemain(ConstIngredients.Instance.Lemon, 2);
    }

    public void RemoveAll() {
        Data.ClearList();
    }

    public override void Bind(KitchenData data) {
        data.KitchenStorage ??= new(_defaultMaxSpace, _defaultIngredients);
        Data = data.KitchenStorage;
        base.Bind(data);
    }
}
