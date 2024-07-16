using UnityEngine;

public class KitchenStorage : SaveableIngredientStorage<KitchenData> {

    [ContextMenu("AddLemon")]
    public void AddLemon() {
        PutIngredientWithRemain(ConstIngredients.Instance.Lemon, 2);
    }

    public void RemoveAll() {
        Data.ClearList();
    }

    public override void Bind(KitchenData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.KitchenStorage = new(_defaultMaxSpace);
        Data = data.KitchenStorage;
        base.Bind(data, isFileEmpty);
    }
}
