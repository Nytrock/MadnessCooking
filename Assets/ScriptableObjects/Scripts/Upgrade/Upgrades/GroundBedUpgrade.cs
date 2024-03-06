using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(GroundBedUpgrade))]
public class GroundBedUpgrade : ProgressUpgrade
{
    [SerializeField] private int _costAdd;

    public int CostAdd => _costAdd;
}
