using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(CountUpgrade))]
public class CountUpgrade : BaseUpgrade {
    [SerializeField, Min(0)] private int _count;

    public int Count => _count;
}
