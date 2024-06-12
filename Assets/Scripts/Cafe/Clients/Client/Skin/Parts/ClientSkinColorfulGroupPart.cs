using UnityEngine;

public class ClientSkinColorfulGroupPart : ClientSkinGroupPart {
    [SerializeField] private Color[] _randomColors;
    [SerializeField] private SpecialClientColor[] _specialColors;

    public override void SetRandomSprite(int spriteIndex) {
        base.SetRandomSprite(spriteIndex);
        int colorIndex = Random.Range(0, _randomColors.Length);
        foreach (var part in _relatedParts) {
            if (part.TryGetComponent(out ClientSkinColorfulPart colorfulPart))
                colorfulPart.SetRandomSprite(spriteIndex, colorIndex);
            else
                part.SetRandomSprite(spriteIndex);
        }
    }
}
