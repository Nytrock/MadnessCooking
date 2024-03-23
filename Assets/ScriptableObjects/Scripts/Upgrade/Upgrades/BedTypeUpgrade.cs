using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(BedTypeUpgrade))]
public class BedTypeUpgrade : GraphUpgrade
{
    [SerializeField] private BedType _bedType;

    public BedType BedType => _bedType;
}
