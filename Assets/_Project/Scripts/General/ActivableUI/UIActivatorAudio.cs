using UnityEngine;

namespace MadnessCooking.General {
    public class UIActivatorAudio : SwitchableAudioSource {
        [SerializeField] private ActivableUI _activable;

        protected override void Awake() {
            base.Awake();
            _activable.StateChanged += SwitchStateAndPlay;
        }
    }
}
