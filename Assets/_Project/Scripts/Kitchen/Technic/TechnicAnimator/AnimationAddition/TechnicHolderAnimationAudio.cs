using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TechnicHolderAnimationAudio : TechnicHolderAnimationAddition {
    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    public override void UpdateAnimation(TechnicHolderData data, bool isTest = false) {
        _audioSource.ChangeState(isTest || data.IsCooking);
    }
}
