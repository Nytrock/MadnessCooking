using UnityEngine;

[CreateAssetMenu(menuName = nameof(BedType))]
public class BedType : BaseObject
{
    public IngredientType AcceptableType;
    public Sprite BedSprite;
}
