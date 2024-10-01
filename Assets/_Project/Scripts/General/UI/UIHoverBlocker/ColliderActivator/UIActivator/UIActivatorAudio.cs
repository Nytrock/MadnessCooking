using UnityEngine;

public class UIActivatorAudio : SwitchableAudioSource {
    [SerializeField] private UIActivator _activator;

    protected override void Awake() {
        base.Awake();
        _activator.StateChanged += SwitchStateAndPlay;
    }
}
