using System;
using UnityEngine;

[Serializable]
public class LocationMusicInfo : AudioInfo {
    [SerializeField] private Location _location;

    public Location Location => _location;
}
