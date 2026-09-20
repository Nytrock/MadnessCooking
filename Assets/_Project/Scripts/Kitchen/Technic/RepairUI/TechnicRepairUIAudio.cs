using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Kitchen {
    public class TechnicRepairUIAudio : SwitchableAudioSource {
        [SerializeField] private TechnicRepairUI _repairUI;

        protected override void Awake() {
            base.Awake();
            _repairUI.StateChanged += SwitchStateAndPlay;
        }
    }
}
