using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour, IBindable<AudioSettingsData>, ISettingable<float> {
    [SerializeField] private AudioMixer _mixer;
    [SerializeField, Range(0, 1)] private float _defaultVolumeCoef;
    private AudioVolumeSettingsData _data;

    public float DefaultValue => _defaultVolumeCoef;

    public void Bind(AudioSettingsData data) {
        data.VolumeSettings ??= new(DefaultValue);
        _data = data.VolumeSettings;
        UpdateValue();
    }

    public void UpdateValue() {
        _mixer.SetFloat("Master", Mathf.Log10(_data.MasterVolume.LastValue) * 20);
        _mixer.SetFloat("UI", Mathf.Log10(_data.UIVolume.LastValue) * 20);
        _mixer.SetFloat("SFX", Mathf.Log10(_data.SfxVolume.LastValue) * 20);
        _mixer.SetFloat("Music", Mathf.Log10(_data.MusicVolume.LastValue) * 20);
    }
}
