using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(Ingredient))]

public class Ingredient : BuyableObject
{
    [SerializeField] private IngredientType _typeIngredient;
    [SerializeField, Min(0)] private int _timeGrow;
    [SerializeField, Min(0)] private int _maxCount;
    [SerializeField, Min(0)] private float _fatigueCount;

    public IngredientType Type => _typeIngredient;
    public int TimeGrow => _timeGrow;
    public int MaxCount => _maxCount;
    public float FatigueCount => _fatigueCount;
}
