using System;
using UnityEngine;

[Serializable]
public class SpecialClientSprite {
    [SerializeField] private Sprite _sprite;
    [SerializeField] private ClientSkinType _client;

    public ClientSkinType SkinType => _client;
    public Sprite Sprite => _sprite;
}
