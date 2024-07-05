using System;
using System.Collections.Generic;
using UnityEngine;

public class KitchenStorage : IngredientStorage<KitchenData> {
    public event Action IngredientsChanged;

    [ContextMenu("AddLemon")]
    public void AddLemon() {
        PutIngredientWithRemain(ConstIngredients.Instance.Lemon, 2);
        IngredientsChanged?.Invoke();
    }

    public override int PutIngredientWithRemain(Ingredient ingredient, int count) {
        int remain = base.PutIngredientWithRemain(ingredient, count);
        IngredientsChanged?.Invoke();
        return remain;
    }

    public override void RemoveIngredients(IEnumerable<BuyableItemCount<Ingredient>> countList) {
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
