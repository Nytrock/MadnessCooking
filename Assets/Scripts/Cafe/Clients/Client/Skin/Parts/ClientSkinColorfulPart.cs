using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClientSkinColorfulPart : ClientSkinPart {
    [SerializeField] private Color[] _randomColors;
    [SerializeField] private SpecialClientColor[] _specialColors;

    public override void SetRandomSprite(int spriteIndex) {
        base.SetRandomSprite(spriteIndex);
        int colorIndex = Random.Range(0, _randomColors.Length);
        _renderer.color = _randomColors[colorIndex];
    }

    public void SetRandomSprite(int spriteIndex, int colorIndex) {
        base.SetRandomSprite(spriteIndex);
        _renderer.color = _randomColors[colorIndex];
    }

    public override void SetSpecialSprite(ClientSkinType skinType) {
        base.SetSpecialSprite(skinType);
        foreach (var specialColor in _specialColors) {
            if (specialColor.SkinType == skinType) {
                _renderer.color = specialColor.Color;
                return;
            }
        }
        throw new ArgumentNullException($"No color for client {skinType}");
    }

    public override bool CheckRelationToGroup(ClientSkinGroupPart groupPart) {
        if (!base.CheckRelationToGroup(groupPart))
            return false;

        foreach (var groupSpecialSprite in groupPart.GetSpecialSprites()) {
            if (!_specialColors.Select(color => color.SkinType).Contains(groupSpecialSprite.SkinType)) {
                return false;
            }
        }

        return true;
    }
}
