using System.Collections.Generic;
using UnityEngine;

namespace MadnessCooking.General {
    [CreateAssetMenu(menuName = AssetMenuName + nameof(Decor))]
    public class Decor : BuyableItem, IGraphable<Decor> {
        [SerializeField, Min(0)] private float _fatigueDecreaseCoef;
        [SerializeField] private Location _location;
        [SerializeField] private Decor[] _needDecor;
        [SerializeField] private Decor[] _nextDecor;

        protected override string _table => nameof(Decor) + "Table";

        public Location Location => _location;
        public float FatigueCoef => _fatigueDecreaseCoef;
        public IEnumerable<Decor> NeedItems => _needDecor;
        public IEnumerable<Decor> NextItems => _nextDecor;
    }
}
