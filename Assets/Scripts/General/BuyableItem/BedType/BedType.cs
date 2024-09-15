using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(BedType))]
public class BedType : BuyableItem {
    [SerializeField] private IngredientType _acceptableType;
    protected override string _table => nameof(BedType) + "Table";

    public IngredientType AcceptableType => _acceptableType;
}
