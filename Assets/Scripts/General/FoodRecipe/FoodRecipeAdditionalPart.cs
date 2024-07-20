using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FoodRecipeAdditionalPart : HoverTextActivator {
    protected GrayscaleImageRenderer _icon;

    private void Awake() {
        _icon.SetImage(GetComponent<Image>());
        ChangeState(false);
    }

    public void ChangeState(bool newState) {
        _icon?.SetActive(newState);
    }
}
