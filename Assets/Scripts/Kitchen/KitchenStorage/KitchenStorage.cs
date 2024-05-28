using System;
using UnityEngine;

public class KitchenStorage : IngredientStorage<KitchenData> {
    public event Action IngredientsChanged;

    [ContextMenu("AddLemon")]
    public void AddLemon() {
        PutIngredientWithRemain(new IngredientCount(ConstIngredients.Instance.Lemon, 2));
        IngredientsChanged?.Invoke();
    }

    public override int PutIngredientWithRemain(IngredientCount puttingCount) {
        int remain = base.PutIngredientWithRemain(puttingCount);
        IngredientsChanged?.Invoke();
        return remain;
    }

    public override void RemoveIngredients(IngredientCountList countList) {
        base.RemoveIngredients(countList);
        IngredientsChanged?.Invoke();
    }

    public void RemoveAll() {
        Data.ClearList();
        IngredientsChanged?.Invoke();
    }

    public override void Bind(KitchenData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.KitchenStorage = new(_defaultMaxSpace);
        Data = data.KitchenStorage;
        base.Bind(data, isFileEmpty);
    }
}
