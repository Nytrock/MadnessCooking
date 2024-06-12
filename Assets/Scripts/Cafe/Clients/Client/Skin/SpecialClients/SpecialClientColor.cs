using System;
using UnityEngine;

[Serializable]
public class SpecialClientColor {
    [SerializeField] private Color _color;
    [SerializeField] private ClientSkinType _client;

    public ClientSkinType SkinType => _client;
    public Color Color => _color;
}
