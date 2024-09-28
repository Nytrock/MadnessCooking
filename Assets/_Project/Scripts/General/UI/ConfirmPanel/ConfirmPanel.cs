using System;
using UnityEngine;

public class ConfirmPanel : MonoBehaviour {
    [SerializeField] private GameObject _panel;
    [SerializeField] private ButtonWithAudio _yesButton;
    [SerializeField] private ButtonWithAudio _noButton;
    [SerializeField] private LocalizedText _titleText;
    [SerializeField] private LocalizedText _confirmText;

    private void Awake() {
        ChangeState(false);
    }

    public void StartConfirm(Action<bool> pressAction, string confirmMessage, string confirmTitle = "ConfirmPanel.YouSure") {
        ChangeState(true);

        _titleText.SetText(confirmTitle);
        _confirmText.SetText(confirmMessage);
        _yesButton.onClick.AddListener(delegate { EndConfirm(); pressAction(true); });
        _noButton.onClick.AddListener(delegate { EndConfirm(); pressAction(false); });
    }

    private void EndConfirm() {
        _yesButton.onClick.RemoveAllListeners();
        _noButton.onClick.RemoveAllListeners();
        ChangeState(false);
    }

    private void ChangeState(bool state) {
        _panel.SetActive(state);
    }
}
