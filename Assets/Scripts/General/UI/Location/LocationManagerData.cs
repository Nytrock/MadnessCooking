using System;
using UnityEngine;

[Serializable]
public class LocationManagerData {
    [SerializeField] private int _startLocationIndex;

    public int StartLocationIndex => _startLocationIndex;

    public LocationManagerData() {
        _startLocationIndex = 0;
    }

    public void ChangeLocation(int newIndex) {
        _startLocationIndex = newIndex;
    }
}