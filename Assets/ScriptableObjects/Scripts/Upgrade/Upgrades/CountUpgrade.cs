using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(CountUpgrade))]
public class CountUpgrade : ProgressUpgrade
{
    [SerializeField] private int _count;

    public int Count => _count;
}
