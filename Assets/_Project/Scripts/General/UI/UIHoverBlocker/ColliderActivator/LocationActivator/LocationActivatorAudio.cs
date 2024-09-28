using UnityEngine;

public class LocationActivatorAudio : SwitchableAudioSource {
    [SerializeField] private LocationActivator _activator;

    protected override void Awake() {
        base.Awake();
        _activator.StateChanged += SwitchState;
    }
}
