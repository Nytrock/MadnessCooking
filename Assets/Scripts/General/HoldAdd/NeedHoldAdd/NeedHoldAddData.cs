using System;
using UnityEngine;

[Serializable]
public class NeedHoldAddData : HoldAddData {
    [SerializeField] private int _materialCount;

    public int MaterialCount => _materialCount;

    public NeedHoldAddData(int readyCount, int materialCount) : base(readyCount) {
        _materialCount = materialCount;
    }

    public void AddMaterial() {
        _materialCount++;
    }

    public void SubstractMaterial() {
        if (_materialCount == 0)
            return;

        _materialCount--;
    }

    public void AddMaterial(int materialCount) {
        _materialCount += materialCount;
    }

    public void SetMaterial(int remainCount) {
        if (remainCount < 0)
            return;

        _materialCount = remainCount;
    }
}
