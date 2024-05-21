using System;
using UnityEngine;

public class KitchenStorage : IngredientStorage<KitchenData>
{
    [SerializeField] private Ingredient _lemon;
    public event Action IngredientsChanged;

    [ContextMenu("AddLemon")]
    public void AddLemon()
    {
        PutIngredient(new IngredientCount(_lemon, 2));
        IngredientsChanged?.Invoke();
    }

    public override void PutIngredient(IngredientCount newElement)
    {
        base.PutIngredient(newElement);
        IngredientsChanged?.Invoke();
    }

    public override void RemoveIngredients(IngredientCountList countList)
    {
        base.RemoveIngredients(countList);
        IngredientsChanged?.Invoke();
    }

    public void RemoveAll()
    {
        Data.ClearList();
        IngredientsChanged?.Invoke();
    }

    public override void Bind(KitchenData data, bool isFileEmpty)
    {
        if (isFileEmpty)
            data.KitchenStorage = new(_defaultMaxSpace);
        Data = data.KitchenStorage;
        base.Bind(data, isFileEmpty);
    }
}
