using System;
using System.Linq;
using UnityEngine;

public class ClientSkinGroupPart : ClientSkinPart {
    [SerializeField] protected ClientSkinPart[] _relatedParts;

    protected override void Awake() {
        base.Awake();
        foreach (var part in _relatedParts) {
            if (!part.CheckRelationToGroup(this)) {
                throw new ArgumentException($"Skin part {part.name} not related to group {name}, " +
                    $"but located in it");
            }
        }
    }

    public override void SetRandomSprite(int spriteIndex) {
        base.SetRandomSprite(spriteIndex);
        foreach (var part in _relatedParts) {
            part.SetRandomSprite(spriteIndex);
        }
    }

    public override void SetSpecialSprite(ClientSkinType skinType) {
        base.SetSpecialSprite(skinType);
        foreach (var part in _relatedParts) {
            part.SetSpecialSprite(skinType);
        }
    }

    public bool IsPartInGroup(ClientSkinPart part) {
        return _relatedParts.Contains(part);
    }
}
