using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SwitchableAudioSource : MonoBehaviour {
    [SerializeField] private AudioInfo _stateTrueAudio;
    [SerializeField] private AudioInfo _stateFalseAudio;

    private AudioSource _audioSource;
    private bool _isFirstState = true;

    protected virtual void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SwitchState(bool state) {
        _isFirstState = state;
        UpdateSounds();
    }

    protected void UpdateSounds() {
        if (_isFirstState)
            _audioSource.SetAudioInfo(_stateTrueAudio);
        else
            _audioSource.SetAudioInfo(_stateFalseAudio);

        _audioSource.Play();
    }
}
