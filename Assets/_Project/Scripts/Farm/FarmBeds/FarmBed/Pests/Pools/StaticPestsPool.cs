using System;
using System.Collections.Generic;
using UnityEngine;

public class StaticPestsPool : PestsPool {
    [SerializeField] private Pest[] _pests;
    [SerializeField] private List<Pest> _freePests = new();

    public void SetupFreePestsList() {
        foreach (var pest in _pests) {
            pest.ChangeState(false);
            _freePests.Add(pest);
        }
    }

    public override Pest GetObject() {
        _prefabIndex = -1;
        Pest pest = CreateObject();

        pest.ChangeState(true);
        pest.Randomize(_localPosition, _globalPosition, _prefabIndex);
        return pest;
    }

    protected override Pest CreateObject() {
        Pest pest;
        if (_prefabIndex == -1) {
            pest = _freePests.GetRandom();
            _prefabIndex = Array.IndexOf(_pests, pest);
        } else {
            pest = _pests[_prefabIndex];
        }

        _freePests.Remove(pest);
        return pest;
    }

    public override void PutObject(Pest pest) {
        base.PutObject(pest);
        _freePests.Add(pest);
    }
}