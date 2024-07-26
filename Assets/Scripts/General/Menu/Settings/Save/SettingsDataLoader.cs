using UnityEngine;

public class SettingsDataLoader : DataLoader<SetttingsData> {
    [SerializeField] private GameSettingsLoader _gameLoader;
    [SerializeField] private AudioSettingsLoader _audioLoader;
    [SerializeField] private VideoSettingsLoader _videoLoader;
    [SerializeField] private SettingsManager _settingsManager;

    public override void Load(SetttingsData data, bool isFileEmpty) {
        _gameLoader.LoadData(data.GameSettings, isFileEmpty);
        _audioLoader.LoadData(data.AudioSettings, isFileEmpty);
        _videoLoader.LoadData(data.VideoSettings, isFileEmpty);

        if (_settingsManager != null)
            _settingsManager.Bind(data, isFileEmpty);
    }
}
