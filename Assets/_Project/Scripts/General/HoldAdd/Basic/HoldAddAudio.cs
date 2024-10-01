using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HoldAddAudio : MonoBehaviour {
    [SerializeField] private HoldAdd _holdAdd;
    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _holdAdd.WorkChanged += _audioSource.ForceChangeState;
    }
}
