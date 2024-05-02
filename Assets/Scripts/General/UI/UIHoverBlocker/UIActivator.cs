using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class UIActivator : MonoBehaviour
{
    [SerializeField] private UIHoverListener _hoverListener;
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

        Press();
    }

    protected abstract void Press();
}
