using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(Decor))]
public class Decor : BuyableItem, IGraphable<Decor> {
    [SerializeField, Min(0)] private float _fatigueDecreaseCoef;
    [SerializeField] private DecorLocation _location;
    [SerializeField] private Decor[] _needDecor;
    [SerializeField] private Decor[] _nextDecor;

    public DecorLocation Location => _location;
    public float FatigueCoef => _fatigueDecreaseCoef;
    public IEnumerable<Decor> NeedItems => _needDecor;
    public IEnumerable<Decor> NextItems => _nextDecor;
}
