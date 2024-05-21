using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(CoefficientFarmBedUpgrade))]
public class CoefficientFarmBedUpgrade : FarmBedUpgrade
{
    [SerializeField, Min(0)] private float _coefficient;

    public float Coefficient => _coefficient;
}
