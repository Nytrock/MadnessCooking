using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteChanger : MonoBehaviour {
    [SerializeField] private Sprite _disabledSprite;
    [SerializeField] private Sprite _activeSprite;

    private bool _isActive;
    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeSpriteState() {
        _isActive = !_isActive;
        UpdateSprite();
    }

    public void ChangeSpriteState(bool isActive) {
        _isActive = isActive;
        UpdateSprite();
    }

    private void UpdateSprite() {
        _spriteRenderer.sprite = _isActive ? _activeSprite : _disabledSprite;
    }
}
