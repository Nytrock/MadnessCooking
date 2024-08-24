using System;
using UnityEngine;

[Serializable]
public class UpgradeTypeSprite {
    [SerializeField] private UpgradeType _type;
    [SerializeField] private Sprite _sprite;

    public UpgradeType Type => _type;
    public Sprite Sprite => _sprite;
}
