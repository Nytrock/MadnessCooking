public class VisibilityChanger : VisualChanger {
    protected override void UpdateVisual() {
        gameObject.SetActive(_isActive);
    }
}
