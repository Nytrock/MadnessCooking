using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class UIActivator : MonoBehaviour
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

    protected virtual void Press()
    {

    }
}
