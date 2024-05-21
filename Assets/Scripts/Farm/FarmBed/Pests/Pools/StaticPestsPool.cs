using System.Collections.Generic;
using UnityEngine;

public class StaticPestsPool : PestsPool
{
    [SerializeField] private List<Pest> _pests;
    private readonly List<Pest> _freePests;

    private void Awake()
    {
        foreach (var pest in _pests)
            _freePests.Add(pest);
    }

    protected override Pest SpawnPest(ref int id)
    {
        if (id == -1)
            id = Random.Range(0, _freePests.Count);
        _freePests.RemoveAt(id);
        return _freePests[id];
    }
}
