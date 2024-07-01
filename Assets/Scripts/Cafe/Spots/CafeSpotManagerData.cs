using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CafeSpotManagerData {
    [SerializeField] private List<SpotData> _spots;

    public IEnumerable<SpotData> Spots => _spots;

    public CafeSpotManagerData() {
        _spots = new();
    }

    public void RemoveSpotAt(int index) {
        _spots.RemoveAt(index);
    }

    public void AddSpot(SpotData newData) {
        _spots.Add(newData);
    }

    public SpotData GetSpot(int index) {
        return _spots[index];
    }
}
