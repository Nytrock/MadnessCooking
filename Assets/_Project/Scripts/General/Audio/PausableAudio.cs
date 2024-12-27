using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PausableAudio : MonoBehaviour {
    private AudioSource _audioSource;

    private void Awake() {
        CheckAudio();
    }

    private void CheckAudio() {
        if (_audioSource != null)
            return;

        _audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        PauseManager.Instance.PauseChanged += ChangeState;
    }

    private void ChangeState(bool isPause) {
        CheckAudio();
        _audioSource.ChangeState(!isPause);
    }
}
