using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HoldActivator : MonoBehaviour
{
    [SerializeField] private UIHoverListener _hoverListener;
    [SerializeField] private HoldAdd _hold;
    private bool _isHover;

    private void Start()
    {
        _hoverListener.OnHover += ChangeMode;
    }

    private void ChangeMode(bool newValue)
    {
        _isHover = newValue;
    }

    private void OnMouseDown()
    {
        if (_isHover)
            return;

        _hold.ChangeWorkMode(true);
    }

    private void OnMouseExit()
    {
        _hold.ChangeWorkMode(false);
    }

    private void OnMouseUp()
    {
        _hold.ChangeWorkMode(false);
    }
}
