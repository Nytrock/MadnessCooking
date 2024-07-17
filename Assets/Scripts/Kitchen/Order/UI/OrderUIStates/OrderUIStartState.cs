using UnityEngine;
using UnityEngine.UI;

public class OrderUIStartState : OrderUIBaseState {
    [SerializeField] private Button _cookButton;

    public void UpdateCookButton(bool newState) {
        _cookButton.interactable = newState;
    }
}
