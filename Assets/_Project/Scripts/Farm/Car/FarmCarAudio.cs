using UnityEngine;

[RequireComponent(typeof(FarmCar))]
public class FarmCarAudio : MonoBehaviour {
    [SerializeField] private SwitchableAudioSource _carAudioSource;
    [SerializeField] private AudioSource _grassAudioSource;
    private FarmCar _car;

    private void Awake() {
        _car = GetComponent<FarmCar>();
        _car.StateChanged += ChangeState;
    }

    private void ChangeState(CarState state) {
        if (state == CarState.Calm)
            return;

        _carAudioSource.SwitchStateAndPlay(state == CarState.Sent);
    }

    public void StopAudio() {
        _carAudioSource.Stop();
    }

    public void PlayGrassAudio() {
        _grassAudioSource.Play();
    }
}
