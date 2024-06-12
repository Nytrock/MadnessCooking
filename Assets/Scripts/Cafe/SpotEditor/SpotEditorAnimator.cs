using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpotEditorAnimator : MonoBehaviour {
    [SerializeField] private SpotEditor _spotEditor;
    [SerializeField] private Sprite _closedSprite;
    [SerializeField] private Sprite _openedSprite;
    private SpriteRenderer _renderer;

    private void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
        _spotEditor.EditorActivated += OpenAnimatoin;
        _spotEditor.EditorDisabled += CloseAnimatoin;
    }

    private void OpenAnimatoin() {
        _renderer.sprite = _openedSprite;
    }

    private void CloseAnimatoin() {
        _renderer.sprite = _closedSprite;
    }
}
