using UnityEngine;

[CreateAssetMenu(menuName = nameof(BuyableObject) + "/" + nameof(Ingredient))]

public class Ingredient : BuyableObject
{
    [SerializeField] private IngredientType _typeIngredient;
    [SerializeField] private int _timeGrow;
    [SerializeField] private int _maxCount;

    public IngredientType TypeIngredient => _typeIngredient;
    public int TimeGrow => _timeGrow;
    public int MaxCount => _maxCount;
}
