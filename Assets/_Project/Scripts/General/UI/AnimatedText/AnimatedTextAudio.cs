using UnityEngine;

public class AnimatedTextAudio : MonoBehaviour {
    [SerializeField] private AnimatedText _text;
    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _text.TextAnimated += _audioSource.Play;
    }
}
