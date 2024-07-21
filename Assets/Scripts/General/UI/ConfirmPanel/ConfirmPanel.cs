using System;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPanel : MonoBehaviour {
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;
    [SerializeField] private LocalizedText _confirmText;

    private void Awake() {
        ChangeState(false);
    }

    public void StartConfirm(Action<bool> pressAction, string confirmMessage) {
        ChangeState(true);

        _confirmText.SetText(confirmMessage);
        _yesButton.onClick.AddListener(delegate { pressAction(true); EndConfirm(); });
        _noButton.onClick.AddListener(delegate { pressAction(false); EndConfirm(); });
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
