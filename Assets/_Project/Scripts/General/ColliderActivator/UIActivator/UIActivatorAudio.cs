using AYellowpaper;
using UnityEngine;

public class UIActivatorAudio : SwitchableAudioSource {
    [SerializeField] private InterfaceReference<IActivable> _activable;

    protected override void Awake() {
        base.Awake();
        _activable.Value.StateChanged += SwitchStateAndPlay;
    }
}
