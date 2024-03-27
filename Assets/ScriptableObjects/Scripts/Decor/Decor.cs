using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(Decor))]
public class Decor : BuyableObject
{
    [SerializeField, Min(0)] private float _fatigueDecreaseCoef;
    [SerializeField] private DecorType _decorType;

    public DecorType DecorType => _decorType;
    public float FatigueCoef => _fatigueDecreaseCoef;
}
