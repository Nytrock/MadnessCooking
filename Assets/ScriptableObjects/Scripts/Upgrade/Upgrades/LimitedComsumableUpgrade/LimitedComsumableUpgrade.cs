using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(LimitedComsumableUpgrade))]
public class LimitedComsumableUpgrade : ProgressUpgrade
{
    [SerializeField] private int _maxCount;

    public int MaxCount => _maxCount;
}
