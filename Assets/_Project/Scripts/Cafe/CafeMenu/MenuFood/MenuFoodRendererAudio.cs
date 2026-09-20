using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    public class MenuFoodRendererAudio : SwitchableAudioSource {
        [SerializeField] private MenuFoodRenderer _renderer;

        protected override void Awake() {
            base.Awake();
            _renderer.BanishedStateChanged += SwitchStateAndPlay;
        }
    }
}
