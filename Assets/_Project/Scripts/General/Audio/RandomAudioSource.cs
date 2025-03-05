using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomAudioSource : MonoBehaviour {
    [SerializeField] private AudioInfo[] _audios;
    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ForceChangeState(bool newState) {
        if (newState)
            RandomizeAudio();
        _audioSource.ForceChangeState(newState);
    }

    private void RandomizeAudio() {
        _audioSource.SetAudioInfo(_audios.GetRandom());
    }
}
