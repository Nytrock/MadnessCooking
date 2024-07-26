using System;
using UnityEngine;

public class SettingsPanelsManager : MonoBehaviour {
    [SerializeField] private SettingsActionButtonsManager _actionButtonsManager;
    [SerializeField] private SettingsPanel _defaultPanel;
    [SerializeField] private ConfirmPanel _submitChangesConfirm;
    [SerializeField] private SettingsPanel _nowPanel;

    public event Action<SettingsPanel> PanelChanged;

    private void Start() {
        SetDefaultPanel();
    }

    public void ChangePanel(SettingsPanel panel) {
        if (_nowPanel != null && _nowPanel.IsSettingsChanged()) {
            _submitChangesConfirm.StartConfirm(SubmitChangesConfirm, "Settings.CancelChangesDescription");
            _nowPanel = panel;
            return;
        }

        _nowPanel = panel;
        PanelChanged?.Invoke(panel);
    }

    private void SubmitChangesConfirm(bool isConfirm) {
        if (isConfirm)
            _actionButtonsManager.SubmitChanges();
        else
            _actionButtonsManager.CancelChanges();

        PanelChanged?.Invoke(_nowPanel);
    }

    public void SetDefaultPanel() {
        ChangePanel(_defaultPanel);
    }
}
