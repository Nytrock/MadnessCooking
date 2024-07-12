using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class ColliderActivator : MonoBehaviour {
    [SerializeField] private UIHoverListener _hoverListener;
    private bool _isHover;

    protected virtual void Awake() {
        _hoverListener.OnHover += ChangeMode;
    }

    private void ChangeMode(bool newValue) {
        _isHover = newValue;
    }

    private void OnMouseDown() {
        if (_isHover)
            return;

        Press();
    }

    protected abstract void Press();
}
