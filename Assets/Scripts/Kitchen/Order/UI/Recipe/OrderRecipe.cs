using UnityEngine;

public class OrderRecipe : FoodRecipe<OrderRecipePart>
{
    [SerializeField] private Ingredient _spice;
    private bool _isAutoSpice;

    public void SetupRecipe(Food food, bool isAutoSpice)
    {
        _isAutoSpice = isAutoSpice;
        SetupRecipe(food);
    }

    public void SetStorages(KitchenStorage ingredients, TechnicManager technic)
    {
        _kitchenStorage = ingredients;
        _technicManager = technic;
    }

    protected override void SetupIngredients(IngredientCountList ingredients, ref bool canCook)
    {
        for (int i = 0; i < ingredients.Size; i++) {
            var ingredientCount = ingredients.Get(i);
            if (ingredientCount.Ingredient == _spice && _isAutoSpice) {
                _canCook &= MoneyManager.instance.MoneyAmount >= ingredientCount.Count * ingredientCount.Ingredient.Cost;
                _recipeParts[i].SetupAutoSpice(ingredientCount.Ingredient, ingredientCount.Count);
            } else {
                bool haveCount = _kitchenStorage.HaveCount(ingredientCount);
                _canCook &= haveCount;
                _recipeParts[i].Setup(ingredientCount, haveCount);
            }
        }
    }

    protected override void SetupTechnic(Technic technic, int index, ref bool canCook)
    {
        bool haveTechnic = _technicManager.HaveTechnic(technic);
        _canCook &= haveTechnic;
        _recipeParts[index].Setup(technic, haveTechnic);
    }

    public override void DisableParts()
    {
        foreach (var part in _recipeParts)
            part.gameObject.SetActive(false);
    }
}
