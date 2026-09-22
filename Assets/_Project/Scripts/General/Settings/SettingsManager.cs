using System;
using UnityEngine;

namespace MadnessCooking.General {
    public class SettingsManager : MonoBehaviour, ISettingable {
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

        public void SetSettings(SettingsData data) {
            _gameSettings.SetSettings(data.GameSettings);
            _audioSettings.SetSettings(data.AudioSettings);
            _videoSettings.SetSettings(data.VideoSettings);

            _gameSettings.UpdateAllPoints();
            _audioSettings.UpdateAllPoints();
            _videoSettings.UpdateAllPoints();
        }
    }
}
