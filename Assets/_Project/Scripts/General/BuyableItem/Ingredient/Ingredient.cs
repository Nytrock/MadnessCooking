using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(Ingredient))]

public class Ingredient : BuyableItem {
    [SerializeField] private Sprite _miniSprite;
    [SerializeField] private IngredientType _typeIngredient;
    [SerializeField, Min(0)] private int _timeGrow;
    [SerializeField, Min(0)] private int _maxCount;
    [SerializeField, Min(0)] private float _fatigueCoef;
    [SerializeField, Min(0)] private float _wasteAmount;

    protected override string _table => nameof(Ingredient) + "Table";

    public Sprite MiniSprite => _miniSprite;
    public IngredientType Type => _typeIngredient;
    public int TimeGrow => _timeGrow;
    public int MaxCount => _maxCount;
    public float FatigueCoef => _fatigueCoef;
    public float WasteAmount => _wasteAmount;
}