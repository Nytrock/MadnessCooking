using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(ConsumableUpgrade))]
public class ConsumableUpgrade : BaseUpgrade {
    [SerializeField, Min(1)] private int _maxCount;

    public int MaxCount => _maxCount;
}
