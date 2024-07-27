using System;
using UnityEngine;

[Serializable]
public class LocationMusicInfo : MusicInfo {
    [SerializeField] private Location _location;

    public Location Location => _location;
}
