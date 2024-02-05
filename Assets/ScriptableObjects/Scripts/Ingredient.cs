using UnityEngine;

[CreateAssetMenu(menuName = nameof(Ingredient))]

public class Ingredient : BuyableObject
{
    public IngredientType TypeIngredient;
    public Sprite IngredientSprite;
    public Sprite PlantSprite;
    public int TimeGrow;
    public int MaxCount;
}
