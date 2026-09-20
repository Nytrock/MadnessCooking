using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    public class ClientSkinColorfulGroupPart : ClientSkinGroupPart {
        [SerializeField] protected Color _defaultColor;
        [SerializeField] private Color[] _randomColors;

        public int RandomColorsCount => _randomColors.Length;

        public override void SetRandomSprite(int spriteIndex) {
            base.SetRandomSprite(spriteIndex);

            int colorIndex = Random.Range(0, _randomColors.Length);
            _renderer.color = _randomColors[colorIndex];

            foreach (var part in _relatedParts) {
                if (part.TryGetComponent(out ClientSkinColorfulPart colorfulPart))
                    colorfulPart.SetRandomSprite(spriteIndex, colorIndex);
                else
                    part.SetRandomSprite(spriteIndex);
            }
        }

        public override void SetSpecialSprite(ClientSkinType skinType) {
            base.SetSpecialSprite(skinType);
            _renderer.color = new(1, 1, 1, 1);
        }
    }
}
