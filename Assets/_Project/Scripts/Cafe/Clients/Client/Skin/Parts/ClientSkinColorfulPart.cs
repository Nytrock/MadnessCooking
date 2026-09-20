using UnityEngine;
using Random = UnityEngine.Random;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    public class ClientSkinColorfulPart : ClientSkinPart {
        [SerializeField] protected Color _defaultColor;
        [SerializeField] protected Color[] _randomColors;

        public int RandomColorsCount => _randomColors.Length;

        public override void SetRandomSprite(int spriteIndex) {
            base.SetRandomSprite(spriteIndex);
            int colorIndex = Random.Range(0, RandomColorsCount);
            _renderer.color = _randomColors[colorIndex];
        }

        public void SetRandomSprite(int spriteIndex, int colorIndex) {
            base.SetRandomSprite(spriteIndex);
            _renderer.color = _randomColors[colorIndex];
        }

        public override void SetSpecialSprite(ClientSkinType skinType) {
            base.SetSpecialSprite(skinType);
            _renderer.color = new(1, 1, 1, 1);
        }

        public override bool CheckRelationToGroup(ClientSkinGroupPart groupPart) {
            if (!base.CheckRelationToGroup(groupPart))
                return false;

            ClientSkinColorfulGroupPart colorfulGroupPart = groupPart as ClientSkinColorfulGroupPart;
            if (colorfulGroupPart == null)
                return false;
            if (RandomColorsCount != colorfulGroupPart.RandomColorsCount)
                return false;

            return true;
        }

        public override void SetDefalult() {
            base.SetDefalult();
            _renderer.color = _defaultColor;
        }
    }
}
