using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SettingsPanelButton : MonoBehaviour {
    [SerializeField] private SettingsPanelsManager _panelsManager;
    [SerializeField] private SettingsPanel _panel;
    private Button _button;

    private void Awake() {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(delegate { _panelsManager.ChangePanel(_panel); });
        _panelsManager.PanelChanged += UpdateButton;
    }

    private void UpdateButton(SettingsPanel panel) {
        _button.interactable = _panel != panel;
        _panel.ChangeState(_panel == panel);

        if (_panel == panel)
            transform.SetAsLastSibling();
    }
}
