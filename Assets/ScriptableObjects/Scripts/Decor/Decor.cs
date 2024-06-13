using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(Decor))]
public class Decor : BuyableObject, IGraphable<Decor> {
    [SerializeField, Min(0)] private float _fatigueDecreaseCoef;
    [SerializeField] private DecorType _decorType;
    [SerializeField] private Decor[] _needDecor;
    [SerializeField] private Decor[] _nextDecor;

    public DecorType DecorType => _decorType;
    public float FatigueCoef => _fatigueDecreaseCoef;

    public IEnumerable<Decor> NeedItems => _needDecor;
    public IEnumerable<Decor> NextItems => _nextDecor;
}
