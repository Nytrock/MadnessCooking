using System;
using UnityEngine;

public class SettingsPanelsManager : MonoBehaviour {
    [SerializeField] private SettingsPanel _defaultPanel;

    public event Action<SettingsPanel> PanelChanged;

    private void Start() {
        SetDefaultPanel();
    }

    public void ChangePanel(SettingsPanel panel) {
        PanelChanged?.Invoke(panel);
    }

    internal void SetDefaultPanel() {
        ChangePanel(_defaultPanel);
    }
}
