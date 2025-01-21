using System.Collections.Generic;

public class IngredientsManager : SaveableItemManager<Ingredient, FarmData> {
    public override bool IsItemAvailable(Ingredient ingredient) {
        if (ingredient.Type == IngredientType.Buyable)
            return true;

        return base.IsItemAvailable(ingredient);
    }

    public bool HaveIngredientsOfBedType(BedType bedType) {
        foreach (var ingredient in _data.AvailableItems)
            if (ingredient.Type == bedType.AcceptableType)
                return true;
        return false;
    }

    public IEnumerable<Ingredient> GetAvailableIngredientsOfBedType(BedType bedType) {
        foreach (var ingredient in _data.AvailableItems)
            if (ingredient.Type == bedType.AcceptableType)
                yield return ingredient;
    }

    public IEnumerable<Ingredient> GetAllIngredientsOfBedType(BedType bedType) {
        foreach (var ingredient in _allItems)
            if (ingredient.Type == bedType.AcceptableType)
                yield return ingredient;
    }

    public override void AddItem(Ingredient item) {
        if (item.Type != IngredientType.Buyable)
            base.AddItem(item);
    }

    public override void Bind(FarmData data) {
        data.IngredientManager ??= new();
        _data = data.IngredientManager;
    }
}
