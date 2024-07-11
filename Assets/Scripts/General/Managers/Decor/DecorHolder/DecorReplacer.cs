using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DecorReplacer : DecorHolder {
    [SerializeField] private Sprite _noDecorSprite;
    [SerializeField] private Sprite _haveDecorSprite;
    private SpriteRenderer _renderer;

    private void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public override void ChangeState(bool newValue) {
        base.ChangeState(newValue);

        if (newValue)
            _renderer.sprite = _haveDecorSprite;
        else
            _renderer.sprite = _noDecorSprite;
    }
}
