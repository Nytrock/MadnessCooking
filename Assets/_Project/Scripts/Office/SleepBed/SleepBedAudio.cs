using UnityEngine;

public class SleepBedAudio : SwitchableAudioSource {
    [SerializeField] private SleepBed _bed;

    protected override void Awake() {
        base.Awake();
        _bed.SleepChanged += SwitchStateAndPlay;
    }
}
