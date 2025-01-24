using UnityEngine;
using UnityEngine.UI;

public class SettingsActionButtonsManager : MonoBehaviour {
    [SerializeField] private SettingsPanelsManager _panelsManager;
    [SerializeField] private SettingsSaveManager _saveManager;
    [SerializeField] private Button _submitButton;
    [SerializeField] private Button _cancelButton;
    private SettingsPanel _nowPanel;

    private void Awake() {
        _panelsManager.PanelChanged += UpdateNowPanel;
    }

    private void UpdateNowPanel(SettingsPanel panel) {
        if (panel == null) return;

        if (_nowPanel != null)
            _nowPanel.SettingsChanged -= UpdateButtons;
        _nowPanel = panel;
        _nowPanel.SettingsChanged += UpdateButtons;
        UpdateButtons();
    }

    private void UpdateButtons() {
        _submitButton.interactable = _nowPanel.IsSettingsChanged();
        _cancelButton.interactable = _nowPanel.IsSettingsChanged();
    }

    public void CancelChanges() {
        _nowPanel.CancelChanges();
    }

    public void SetDefaultValues() {
        _nowPanel.SetDefaultValues();
    }

    public void SubmitChanges() {
        _nowPanel.SubmitChanges();
        _saveManager.Save();
    }
}
