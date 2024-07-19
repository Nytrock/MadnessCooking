using System;
using UnityEngine;

[Serializable]
public class DecorLocationImage {
    [SerializeField] private DecorLocation _location;
    [SerializeField] private Sprite _sprite;

    public DecorLocation Location => _location;
    public Sprite Sprite => _sprite;
}
