using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FarmBedManagerData : SpaceManagerData {
    [SerializeField] private FarmBedData[] _farmBeds;

    public IEnumerable<FarmBedData> FarmBeds => _farmBeds;

    public FarmBedManagerData(int defaultSpaceCount, int bedsCount) : base(defaultSpaceCount) {
        _farmBeds = new FarmBedData[bedsCount];
        for (int i = 0; i < bedsCount; i++)
            _farmBeds[i] = new();
    }

    public FarmBedData GetBedData(int index) {
        return _farmBeds[index];
    }

}
