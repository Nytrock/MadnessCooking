using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(CountUpgrade))]
public class CountUpgrade : GraphUpgrade
{
    [SerializeField, Min(0)] private int _count;

    public int Count => _count;
}
