using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PausableAudio : MonoBehaviour {
    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        PauseManager.Instance.PauseChanged += ChangeState;
    }

    private void ChangeState(bool isPause) {
        _audioSource.ChangeState(!isPause);
    }
}
