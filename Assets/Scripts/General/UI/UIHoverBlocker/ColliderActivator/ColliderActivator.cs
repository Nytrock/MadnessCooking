using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class ColliderActivator : MonoBehaviour {
    [SerializeField] private UIHoverListener _hoverListener;

    private void OnMouseDown() {
        if (_hoverListener.IsHover || FatigueManager.Instance.IsTired)
            return;

        Press();
    }

    protected abstract void Press();
}
