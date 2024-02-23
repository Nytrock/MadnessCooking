using UnityEngine;

[CreateAssetMenu(menuName = nameof(BuyableObject) + "/" + nameof(BedType))]
public class BedType : BuyableObject
{
    [SerializeField] private IngredientType _acceptableType;

    public IngredientType AcceptableType => _acceptableType;
}
