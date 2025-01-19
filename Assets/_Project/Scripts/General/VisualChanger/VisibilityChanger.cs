using UnityEngine;

public class VisibilityChanger : VisualChanger {
    [SerializeField] private bool _isReversed;

    protected override void UpdateVisual() {
        gameObject.SetActive(_isActive != _isReversed);
    }
}
