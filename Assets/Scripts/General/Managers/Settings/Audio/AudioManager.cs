using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour, IBindable<AudioSettingsData>, ISettingable<float> {
    [SerializeField] private AudioMixer _mixer;
    [SerializeField, Range(0, 1)] private float _defaultVolumeCoef;
    [SerializeField] private float _minValue;
    [SerializeField] private float _maxValue;
    private AudioVolumeSettingsData _data;

    public float DefaultValue => _defaultVolumeCoef;

    public void Bind(AudioSettingsData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.VolumeSettings = new(DefaultValue);
        _data = data.VolumeSettings;
        UpdateValue();
    }

    public void UpdateValue() {
        _mixer.SetFloat("Master", Mathf.Lerp(_minValue, _maxValue, _data.MasterVolume.LastValue));
        _mixer.SetFloat("UI", Mathf.Lerp(_minValue, _maxValue, _data.UIVolume.LastValue));
        _mixer.SetFloat("SFX", Mathf.Lerp(_minValue, _maxValue, _data.SfxVolume.LastValue));
        _mixer.SetFloat("Music", Mathf.Lerp(_minValue, _maxValue, _data.MusicVolume.LastValue));
    }
}
