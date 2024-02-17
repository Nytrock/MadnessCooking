using System.Collections.Generic;
using UnityEngine;

public class StaticPestsPool : PestsPool
{
    [SerializeField] private List<Pest> _pests;
    private readonly List<Pest> _freePests;

    private void Start()
    {
        foreach (var pest in _pests)
            _freePests.Add(pest);
    }

    protected override Pest SpawnPest()
    {
        var index = Random.Range(0, _freePests.Count);
        _freePests.RemoveAt(index);
        return _freePests[index];
    }
}
