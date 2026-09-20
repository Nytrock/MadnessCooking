using AYellowpaper;
using UnityEngine;

namespace MadnessCooking.General {
    public class UIActivatorAudio : SwitchableAudioSource {
        [SerializeField] private InterfaceReference<IStateable> _activable;

        protected override void Awake() {
            base.Awake();
            _activable.Value.StateChanged += SwitchStateAndPlay;
        }
    }
}
