using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour, IBindable<AudioSettingsData>, ISettingable<float> {
    [SerializeField] private AudioMixer _mixer;
    [SerializeField, Range(0, 1)] private float _defaultVolumeCoef;
    [SerializeField] private RangeFloat _volume;
    private AudioVolumeSettingsData _data;

    public float DefaultValue => _defaultVolumeCoef;

    public void Bind(AudioSettingsData data) {
        data.VolumeSettings ??= new(DefaultValue);
        _data = data.VolumeSettings;
        UpdateValue();
    }

    public void UpdateValue() {
        _mixer.SetFloat("Master", _volume.Lerp(_data.MasterVolume.LastValue));
        _mixer.SetFloat("UI", _volume.Lerp(_data.UIVolume.LastValue));
        _mixer.SetFloat("SFX", _volume.Lerp(_data.SfxVolume.LastValue));
        _mixer.SetFloat("Music", _volume.Lerp(_data.MusicVolume.LastValue));
    }
}
