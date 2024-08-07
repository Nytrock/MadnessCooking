using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FoodRecipeAdditionalPart : HoverTextActivator {
    protected GrayscaleImageRenderer _icon;

    protected void Awake() {
        _icon = new();
        _icon.SetImage(GetComponent<Image>());

        ChangeState(false);
    }

    public void ChangeState(bool newState) {
        _icon?.SetActive(newState);
    }
}
