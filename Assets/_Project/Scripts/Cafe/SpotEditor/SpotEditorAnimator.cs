using UnityEngine;

namespace MadnessCooking.Cafe {
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpotEditorAnimator : MonoBehaviour {
        [SerializeField] private SpotEditor _spotEditor;
        [SerializeField] private Sprite _closedSprite;
        [SerializeField] private Sprite _openedSprite;
        private SpriteRenderer _renderer;

        private void Awake() {
            _renderer = GetComponent<SpriteRenderer>();
            _spotEditor.EditorActivated += OpenAnimation;
            _spotEditor.EditorDisabled += CloseAnimation;
        }

        private void OpenAnimation() {
            _renderer.sprite = _openedSprite;
        }

        private void CloseAnimation() {
            _renderer.sprite = _closedSprite;
        }
    }
}
