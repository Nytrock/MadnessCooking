using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HoldActivator : MonoBehaviour {
    [SerializeField] private UIHoverListener _hoverListener;
    [SerializeField] private HoldAdd _hold;
    private bool _isHover;
    private bool _isMouseDown;

    private void Start() {
        _hoverListener.OnHover += ChangeMode;
    }

    private void ChangeMode(bool newValue) {
        _isHover = newValue;
    }

    private void OnMouseDown() {
        if (_isHover)
            return;

        _isMouseDown = true;
        _hold.ChangeWorkMode(true);
    }

    private void OnMouseExit() {
        if (!_isMouseDown)
            return;

        _isMouseDown = false;
        _hold.ChangeWorkMode(false);
    }

    private void OnMouseUp() {
        _isMouseDown = false;
        _hold.ChangeWorkMode(false);
    }
}
