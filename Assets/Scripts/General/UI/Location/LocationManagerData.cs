using System;
using UnityEngine;

[Serializable]
public class LocationManagerData {
    [SerializeField] private int _startLocationIndex = 0;

    public int StartLocationIndex => _startLocationIndex;

    public void ChangeLocation(int newIndex) {
        _startLocationIndex = newIndex;
    }
}