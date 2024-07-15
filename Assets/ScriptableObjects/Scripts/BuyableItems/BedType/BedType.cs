using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(BedType))]
public class BedType : BuyableItem {
    [SerializeField] private IngredientType _acceptableType;

    public IngredientType AcceptableType => _acceptableType;
}
