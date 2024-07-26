using UnityEngine;

public class AudioSettings : SettingsPanel, IBindable<AudioSettingsData> {
    [SerializeField] private AudioSettingsPoint _masterAudio;
    [SerializeField] private AudioSettingsPoint _UIAudio;
    [SerializeField] private AudioSettingsPoint _sfxAudio;
    [SerializeField] private AudioSettingsPoint _musicAudio;

    public void Bind(AudioSettingsData data, bool isFileEmpty) {
        _masterAudio.Bind(data.VolumeSettings.MasterVolume, isFileEmpty);
        _UIAudio.Bind(data.VolumeSettings.UIVolume, isFileEmpty);
        _sfxAudio.Bind(data.VolumeSettings.SfxVolume, isFileEmpty);
        _musicAudio.Bind(data.VolumeSettings.MusicVolume, isFileEmpty);
    }

    protected override void GenerateSettingPointsArray() {
        _settingPoints = new BaseSettingsPoint[] { _masterAudio, _UIAudio, _sfxAudio, _musicAudio };
    }
}
