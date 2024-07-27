using UnityEngine;

public class SettingsManager : MonoBehaviour, ILoadable<SettingsData> {
    [SerializeField] private GameObject _panel;
    [SerializeField] private SettingsPanelsManager _panelsManager;

    [Header("Settings")]
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private AudioSettings _audioSettings;
    [SerializeField] private VideoSettings _videoSettings;

    private void Awake() {
        ChangeState(false);
    }

    public void ChangeState() {
        _panel.SetActive(!_panel.activeSelf);
        if (_panel.activeSelf)
            _panelsManager.SetDefaultPanel();
    }

    private void ChangeState(bool newState) {
        _panel.SetActive(newState);
        if (newState)
            _panelsManager.SetDefaultPanel();
    }

    public void Load(SettingsData data, bool isFileEmpty) {
        _gameSettings.Bind(data.GameSettings, isFileEmpty);
        _audioSettings.Bind(data.AudioSettings, isFileEmpty);
        _videoSettings.Bind(data.VideoSettings, isFileEmpty);
    }
}
