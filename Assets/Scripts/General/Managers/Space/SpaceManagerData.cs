using System;
using UnityEngine;

[Serializable]
public class SpaceManagerData {
    [SerializeField] private int _count;

    public int Count => _count;

    public SpaceManagerData(int defaultSpaceCount) {
        _count = defaultSpaceCount;
    }

    public void SetCount(CountUpgrade countUpgrade) {
        _count = countUpgrade.Count;
    }
}
