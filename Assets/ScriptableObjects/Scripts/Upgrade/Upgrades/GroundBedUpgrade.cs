using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(GroundBedUpgrade))]
public class GroundBedUpgrade : GraphUpgrade
{
    [SerializeField] private int _costAdd;
    [SerializeField] private BedType[] _suitableBedTypes;

    public int CostAdd => _costAdd;
    public BedType[] SuitableBedTypes => _suitableBedTypes;
}
