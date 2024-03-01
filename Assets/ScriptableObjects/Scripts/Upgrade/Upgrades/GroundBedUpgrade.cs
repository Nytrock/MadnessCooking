using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(GroundBedUpgrade))]
public class GroundBedUpgrade : BaseUpgrade
{
    [SerializeField] private int _costAdd;

    public int CostAdd => _costAdd;
}
