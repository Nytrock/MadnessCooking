using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HoldActivator : MonoBehaviour {
    [SerializeField] private UIHoverListener _hoverListener;
    [SerializeField] private HoldAdd _hold;
    private bool _isMouseDown;

    private void OnMouseDown() {
        if (_hoverListener.IsHover || !_hold.IsUnlocked)
            return;

        _isMouseDown = true;
        _hold.ChangeClickMode(true);
    }

    private void OnMouseExit() {
        if (!_isMouseDown || !_hold.IsUnlocked)
            return;

        _isMouseDown = false;
        _hold.ChangeClickMode(false);
    }

    private void OnMouseUp() {
        if (_hoverListener.IsHover || !_hold.IsUnlocked)
            return;

        _isMouseDown = false;
        _hold.ChangeClickMode(false);
    }
}
