using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(GraphUpgrade))]
public class GraphUpgrade : BaseUpgrade
{
    [SerializeField] private GraphUpgrade[] _needUpgrades;
    [SerializeField] private GraphUpgrade[] _nextUpgrades;

    public GraphUpgrade[] NeedUpgrades => _needUpgrades;
    public GraphUpgrade[] NextUpgrades => _nextUpgrades;
}
