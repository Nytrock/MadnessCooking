using UnityEngine;

public class DynamicPestsPool : PestsPool {
    [SerializeField] private Pest[] _prefabs;

    protected override Pest CreateObject() {
        if (_lastId == -1)
            _lastId = Random.Range(0, _prefabs.Length);
        Pest pest = Instantiate(_prefabs[_lastId], _container);
        return pest;
    }
}
