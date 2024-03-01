using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(BedType))]
public class BedType : BuyableObject
{
    [SerializeField] private IngredientType _acceptableType;

    public IngredientType AcceptableType => _acceptableType;
}
