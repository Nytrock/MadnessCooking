using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(FarmBedUpgrade))]
public class FarmBedUpgrade : GraphUpgrade {
    public new const string AssetMenuName = BaseUpgrade.AssetMenuName + "FarmBedUpgrades/";

    [SerializeField, Min(0)] private int _priceAdd;
    [SerializeField, Min(0)] private float _fatigueCoef;
    [SerializeField] private BedType[] _suitableBedTypes;

    public int PriceAdd => _priceAdd;
    public float FatigueCoef => _fatigueCoef;
    public BedType[] SuitableBedTypes => _suitableBedTypes;
}
