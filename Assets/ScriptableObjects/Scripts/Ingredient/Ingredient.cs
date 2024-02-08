using UnityEngine;

[CreateAssetMenu(menuName = nameof(Ingredient))]

public class Ingredient : BuyableObject
{
    public IngredientType TypeIngredient;
    public int TimeGrow;
    public int MaxCount;
}
