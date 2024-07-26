using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour {
    private Button _button;

    public void Awake() {
        _button = GetComponent<Button>();
    }

    public void SetInteractable(bool interactable) {
        _button.interactable = interactable;
    }
}
