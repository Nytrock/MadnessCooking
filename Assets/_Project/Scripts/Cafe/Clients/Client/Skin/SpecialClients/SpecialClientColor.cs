using System;
using UnityEngine;

[Serializable]
public class SpecialClientColor {
    [SerializeField] private Color _color = new(1, 1, 1, 1);
    [SerializeField] private ClientSkinType _client;

    public ClientSkinType SkinType => _client;
    public Color Color => _color;
}
