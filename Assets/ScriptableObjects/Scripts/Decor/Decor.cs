using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(Decor))]
public class Decor : BuyableObject
{
    [SerializeField] private DecorType _decorType;

    public DecorType DecorType => _decorType;
}
