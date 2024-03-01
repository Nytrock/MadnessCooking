using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(ProgressUpgrade))]
public class ProgressUpgrade : BaseUpgrade
{
    [SerializeField] private ProgressUpgrade _nextUpgrade;

    public ProgressUpgrade nextUpgrade => _nextUpgrade;
}
