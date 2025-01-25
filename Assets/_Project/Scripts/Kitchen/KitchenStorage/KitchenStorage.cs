using UnityEngine;

public class KitchenStorage : SaveableIngredientStorage<KitchenData> {
    [SerializeField] private int _lemonCount;
    [SerializeField] private int _spiceCount;

    [ContextMenu(nameof(AddLemon))]
    public void AddLemon() {
        PutIngredientWithRemain(ConstIngredients.Instance.Lemon, _lemonCount);
    }

    [ContextMenu(nameof(RemoveLemon))]
    public void RemoveLemon() {
        RemoveIngredient(ConstIngredients.Instance.Lemon, _lemonCount);
    }

    [ContextMenu(nameof(AddSpice))]
    public void AddSpice() {
        PutIngredientWithRemain(ConstIngredients.Instance.Spice, _spiceCount);
    }

    [ContextMenu(nameof(RemoveSpice))]
    public void RemoveSpice() {
        RemoveIngredient(ConstIngredients.Instance.Spice, _spiceCount);
    }

    public override void Bind(KitchenData data) {
        data.KitchenStorage ??= new(_defaultIngredients);
        Data = data.KitchenStorage;
    }
}
