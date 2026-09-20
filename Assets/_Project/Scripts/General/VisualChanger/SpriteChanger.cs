using UnityEngine;

namespace MadnessCooking.General {
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteChanger : VisualChanger {
        [SerializeField] private Sprite _disabledSprite;
        [SerializeField] private Sprite _activeSprite;
        private SpriteRenderer _spriteRenderer;

        private void Awake() {
            CheckSpriteRenderer();
        }

        protected override void UpdateVisual() {
            CheckSpriteRenderer();
            _spriteRenderer.sprite = _isActive ? _activeSprite : _disabledSprite;
        }

        private void CheckSpriteRenderer() {
            if (_spriteRenderer != null) return;

            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
