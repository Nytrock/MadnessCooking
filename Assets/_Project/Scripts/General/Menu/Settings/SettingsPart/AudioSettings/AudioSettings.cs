using UnityEngine;

public class AudioSettings : SettingsPanel, IBindable<AudioSettingsData> {
    [SerializeField] private FloatSettingsPoint _masterAudio;
    [SerializeField] private FloatSettingsPoint _UIAudio;
    [SerializeField] private FloatSettingsPoint _sfxAudio;
    [SerializeField] private FloatSettingsPoint _musicAudio;

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
