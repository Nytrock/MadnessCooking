using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(CountUpgrade))]
public class CountUpgrade : ProgressUpgrade
{
    [SerializeField] private int _needCount;

    public int NeedCount => _needCount;
}
