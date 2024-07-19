using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(FarmBedUpgrade))]
public class FarmBedUpgrade : BaseUpgrade {
    public new const string AssetMenuName = BaseUpgrade.AssetMenuName + "FarmBedUpgrades/";

    [SerializeField, Min(0)] private int _priceAdd;
    [SerializeField, Min(0)] private float _fatigueCoef;
    [SerializeField] private BedType[] _suitableBedTypes;

    public int PriceToAdd => _priceAdd;
    public float FatigueCoef => _fatigueCoef;
    public IEnumerable<BedType> SuitableBedTypes => _suitableBedTypes;
    public override UpgradeType Type => UpgradeType.FarmBed;
}
