using UnityEngine;

public class AudioSettings : SettingsPanel, IBindable<AudioSettingsData> {
    [SerializeField] private AudioSettingsPoint _masterAudio;
    [SerializeField] private AudioSettingsPoint _UIAudio;
    [SerializeField] private AudioSettingsPoint _sfxAudio;
    [SerializeField] private AudioSettingsPoint _musicAudio;

    public void LateStart() { }

    public void Bind(AudioSettingsData data) {
        _masterAudio.Bind(data.VolumeSettings.MasterVolume);
        _UIAudio.Bind(data.VolumeSettings.UIVolume);
        _sfxAudio.Bind(data.VolumeSettings.SfxVolume);
        _musicAudio.Bind(data.VolumeSettings.MusicVolume);
    }

    protected override void GenerateSettingPointsArray() {
        _settingPoints = new BaseSettingsPoint[] { _masterAudio, _UIAudio, _sfxAudio, _musicAudio };
    }
}
