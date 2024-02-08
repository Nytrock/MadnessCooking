using UnityEngine;

[CreateAssetMenu(menuName = nameof(BedType))]
public class BedType : BuyableObject
{
    public IngredientType AcceptableType;
}
