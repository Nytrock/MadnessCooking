using System;
using UnityEngine;

[Serializable]
public class LocationSprite {
    [SerializeField] private Location _location;
    [SerializeField] private Sprite _sprite;

    public Location Location => _location;
    public Sprite Sprite => _sprite;
}
