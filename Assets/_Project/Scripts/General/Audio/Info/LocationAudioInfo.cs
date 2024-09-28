using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class LocationAudioInfo : AudioInfo {
    [SerializeField] private Location[] _locations;

    public bool ContainsLocation(Location location) {
        return _locations.Contains(location);
    }
}
