using UnityEngine;

namespace MadnessCooking.General {
    public class AudioSettings : SettingsPanel {
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private FloatSettingsPoint _masterAudio;
        [SerializeField] private FloatSettingsPoint _UIAudio;
        [SerializeField] private FloatSettingsPoint _sfxAudio;
        [SerializeField] private FloatSettingsPoint _musicAudio;

        public void SetSettings(AudioSettingsData data) {
            _masterAudio.SetSettingable(_audioManager);
            _UIAudio.SetSettingable(_audioManager);
            _sfxAudio.SetSettingable(_audioManager);
            _musicAudio.SetSettingable(_audioManager);

            _masterAudio.SetData(data.VolumeSettings.MasterVolume);
            _UIAudio.SetData(data.VolumeSettings.UIVolume);
            _sfxAudio.SetData(data.VolumeSettings.SfxVolume);
            _musicAudio.SetData(data.VolumeSettings.MusicVolume);
        }

        protected override void GenerateSettingPointsArray() {
            _settingPoints = new BaseSettingsPoint[] { _masterAudio, _UIAudio, _sfxAudio, _musicAudio };
        }
    }
}
