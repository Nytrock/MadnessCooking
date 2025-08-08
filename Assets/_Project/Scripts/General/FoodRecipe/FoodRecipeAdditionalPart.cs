using UnityEngine;

[RequireComponent(typeof(GrayscaleImage))]
public class FoodRecipeAdditionalPart : HoverItemNameActivator {
    protected GrayscaleImage _grayscaleIcon;
    protected bool _isAvailable;

    public bool IsAvailable => _isAvailable;

    protected override void Awake() {
        base.Awake();
        InitializeIcon();
    }

    protected void InitializeIcon() {
        _grayscaleIcon = GetComponent<GrayscaleImage>();
    }

    public void ChangeState(bool newState) {
        if (_grayscaleIcon == null)
            InitializeIcon();

        _grayscaleIcon.SetActive(newState);
    }
}
