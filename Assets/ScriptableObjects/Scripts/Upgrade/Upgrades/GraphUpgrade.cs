using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(GraphUpgrade))]
public class GraphUpgrade : BaseUpgrade, IGraphable<GraphUpgrade> {
    [SerializeField] private GraphUpgrade[] _needUpgrades;
    [SerializeField] private GraphUpgrade[] _nextUpgrades;

    public IEnumerable<GraphUpgrade> NeedItems => _needUpgrades;
    public IEnumerable<GraphUpgrade> NextItems => _nextUpgrades;
}
