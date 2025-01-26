using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PauseAudio : MonoBehaviour {
    [SerializeField] private PauseManager _manager;
    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _manager.PauseChanged += delegate { SetupAudio(); };
    }

    private void SetupAudio() {
        _manager.PauseChanged -= delegate { SetupAudio(); };
        _manager.PauseChanged += delegate { _audioSource.Play(); };
    }
}
