using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TableFoodView : MonoBehaviour {
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private string _animationName = "isShow";
    private Animator _animator;

    private void Awake() {
        ResetSprite();
        _animator = GetComponent<Animator>();
    }

    public void ShowSprite(Sprite newSprite) {
        _spriteRenderer.sprite = newSprite;
        _animator.SetBool(_animationName, true);
    }

    public void HideSprite() {
        _animator.SetBool(_animationName, false);
    }

    public void ResetSprite() {
        _spriteRenderer.sprite = null;
    }
}
