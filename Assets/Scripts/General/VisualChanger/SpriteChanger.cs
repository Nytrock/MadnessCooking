using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteChanger : VisualChanger {
    [SerializeField] private Sprite _disabledSprite;
    [SerializeField] private Sprite _activeSprite;
    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void UpdateVisual() {
        _spriteRenderer.sprite = _isActive ? _activeSprite : _disabledSprite;
    }
}
