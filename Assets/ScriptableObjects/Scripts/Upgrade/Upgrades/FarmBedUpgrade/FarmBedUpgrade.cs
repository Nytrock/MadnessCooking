using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(FarmBedUpgrade))]
public class FarmBedUpgrade : GraphUpgrade
{
    public new const string AssetMenuName = BaseUpgrade.AssetMenuName + "FarmBedUpgrades/";

    [SerializeField] private int _costAdd;
    [SerializeField, Min(0)] private float _fatigueCoef;
    [SerializeField] private BedType[] _suitableBedTypes;

    public int CostAdd => _costAdd;
    public float FatigueCoef => _fatigueCoef;
    public BedType[] SuitableBedTypes => _suitableBedTypes;
}
