using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(LimitedConsumableUpgrade))]
public class LimitedConsumableUpgrade : GraphUpgrade
{
    [SerializeField, Min(1)] private int _maxCount;

    public int MaxCount => _maxCount;
}
