using UnityEngine;

public class OfficeBedAudio : SwitchableAudioSource {
    [SerializeField] private OfficeBed _bed;

    protected override void Awake() {
        base.Awake();
        _bed.SleepChanged += SwitchState;
    }
}
