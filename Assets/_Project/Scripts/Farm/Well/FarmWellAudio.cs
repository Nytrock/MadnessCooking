using UnityEngine;

[RequireComponent(typeof(FarmWell))]
public class FarmWellAudio : MonoBehaviour {
    [SerializeField] private SwitchableAudioSource _audioSource;

    private FarmWell _well;

    private void Awake() {
        _well = GetComponent<FarmWell>();
        _well.SpeedChanged += ChangeState;
    }

    private void Start() {
        _audioSource.SwitchState(true);
    }

    private void ChangeState(float newSpeed) {
        _audioSource.SwitchState(newSpeed > 0);
    }

    public void PlayAudio() {
        _audioSource.Play();
    }
}
