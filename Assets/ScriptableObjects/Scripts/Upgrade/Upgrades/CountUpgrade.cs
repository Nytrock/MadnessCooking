using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(CountUpgrade))]
public class CountUpgrade : GraphUpgrade
{
    [SerializeField] private int _count;

    public int Count => _count;
}
