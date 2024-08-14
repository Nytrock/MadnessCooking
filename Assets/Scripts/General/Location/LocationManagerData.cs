using System;
using UnityEngine;

[Serializable]
public class LocationManagerData {
    [SerializeField] private Location _startLocation;

    public Location StartLocation => _startLocation;

    public LocationManagerData() {
        _startLocation = Location.Cafe;
    }

    public void ChangeLocation(Location newLocation) {
        _startLocation = newLocation;
    }
}