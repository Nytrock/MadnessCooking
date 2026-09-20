using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    public class DynamicPestsPool : PestsPool {
        [SerializeField] private Pest[] _prefabs;

        protected override Pest CreateObject() {
            if (_prefabIndex == -1 || _prefabIndex >= _prefabs.Length)
                _prefabIndex = Random.Range(0, _prefabs.Length);
            Pest pest = Instantiate(_prefabs[_prefabIndex], _container);
            return pest;
        }
    }
}
