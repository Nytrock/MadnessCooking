using System.Collections.Generic;
using UnityEngine;

public class StaticPestsPool : PestsPool {
    [SerializeField] private Pest[] _pests;
    private readonly List<Pest> _freePests = new();

    private void Awake() {
        foreach (var pest in _pests) {
            pest.Awake();
            pest.ChangeState(false);
            _freePests.Add(pest);
        }
    }

    protected override Pest CreateObject() {
        if (_prefabIndex == -1)
            _prefabIndex = Random.Range(0, _freePests.Count);
        _freePests.RemoveAt(_prefabIndex);
        return _freePests[_prefabIndex];
    }
}
