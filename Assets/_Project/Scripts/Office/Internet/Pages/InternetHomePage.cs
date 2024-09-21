using UnityEngine;
using UnityEngine.UI;

public class InternetHomePage : InternetPage {
    [SerializeField] private Button _homeButton;

    public override void ChangeState(bool newValue) {
        base.ChangeState(newValue);
        _homeButton.interactable = !newValue;
    }
}
