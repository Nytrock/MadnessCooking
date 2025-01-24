using System;
using UnityEngine;

public class SettingsManager : MonoBehaviour, IBindable<SettingsData> {
    [SerializeField] private GameObject _panel;
    [SerializeField] private SettingsPanelsManager _panelsManager;

    [Header("Settings")]
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private AudioSettings _audioSettings;
    [SerializeField] private VideoSettings _videoSettings;

    public event Action SettingsClosed;

    private void Awake() {
        _panel.SetActive(false);
        _panelsManager.PanelChanged += CheckPanel;
    }

    private void CheckPanel(SettingsPanel panel) {
        if (panel == null)
            SettingsClosed?.Invoke();
    }

    public void ChangeState() {
        _panel.SetActive(!_panel.activeSelf);
        if (_panel.activeSelf)
            _panelsManager.SetDefaultPanel();
    }

    public void LateStart() {
        _gameSettings.LateStart();
        _audioSettings.LateStart();
        _videoSettings.LateStart();
    }

    public void Bind(SettingsData data) {
        _gameSettings.Bind(data.GameSettings);
        _audioSettings.Bind(data.AudioSettings);
        _videoSettings.Bind(data.VideoSettings);
    }
}
