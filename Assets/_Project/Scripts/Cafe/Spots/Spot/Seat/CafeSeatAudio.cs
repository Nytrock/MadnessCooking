using UnityEngine;

public class CafeSeatAudio : SwitchableAudioSource {
    [SerializeField] private CafeSeat _seat;

    protected override void Awake() {
        base.Awake();
        _seat.StateChanged += SwitchStateAndPlay;
    }
}
