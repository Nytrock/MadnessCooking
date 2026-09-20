using UnityEngine;
using Random = UnityEngine.Random;

namespace MadnessCooking.Cafe {
    [RequireComponent(typeof(SpriteRenderer))]
    public class ClientSkinPart : MonoBehaviour {
        [SerializeField] protected Sprite _defaultSprite;
        [SerializeField] private Sprite[] _randomSprites;
        [SerializeField] protected SpecialClientSprite[] _specialSprites;

        protected SpriteRenderer _renderer;

        public int RandomSpritesCount => _randomSprites.Length;

        protected virtual void Awake() {
            GetRenderer();
        }

        private void GetRenderer() {
            if (_renderer)
                return;

            _renderer = GetComponent<SpriteRenderer>();
        }

        public void SetSprite(ClientSkinType skinType) {
            if (skinType != ClientSkinType.Random) {
                SetSpecialSprite(skinType);
                return;
            }

            int spriteIndex = Random.Range(0, RandomSpritesCount);
            SetRandomSprite(spriteIndex);
        }

        public virtual void SetRandomSprite(int spriteIndex) {
            GetRenderer();
            if (RandomSpritesCount == 0) {
                _renderer.sprite = _defaultSprite;
                return;
            }

            _renderer.sprite = _randomSprites[spriteIndex];
        }

        public virtual void SetSpecialSprite(ClientSkinType skinType) {
            GetRenderer();
            foreach (var specialSprite in _specialSprites) {
                if (specialSprite.SkinType == skinType) {
                    _renderer.sprite = specialSprite.Sprite;
                    return;
                }
            }

            _renderer.sprite = _defaultSprite;
        }

        public virtual bool CheckRelationToGroup(ClientSkinGroupPart groupPart) {
            if (groupPart.RandomSpritesCount != RandomSpritesCount && RandomSpritesCount != 0)
                return false;
            return true;
        }

        public virtual void SetDefalult() {
            GetRenderer();
            _renderer.sprite = _defaultSprite;
        }
    }
}