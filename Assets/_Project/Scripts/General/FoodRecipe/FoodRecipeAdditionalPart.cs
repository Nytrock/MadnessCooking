using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FoodRecipeAdditionalPart : HoverItemNameActivator {
    protected GrayscaleImageRenderer _icon;
    protected bool _isAvailable;

    public bool IsAvailable => _isAvailable;

    protected virtual void Awake() {
        InitializeIcon();
    }

    protected void InitializeIcon() {
        _icon = new();
        _icon.SetImage(GetComponent<Image>());
    }

    public void ChangeState(bool newState) {
        if (_icon is null)
            InitializeIcon();

        _icon.SetActive(newState);
    }
}
