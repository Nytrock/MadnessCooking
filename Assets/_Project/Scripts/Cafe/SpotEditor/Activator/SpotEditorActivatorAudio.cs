using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    public class SpotEditorActivatorAudio : SwitchableAudioSource {
        [SerializeField] private SpotEditor _editor;

        protected override void Awake() {
            base.Awake();

            _editor.EditorActivated += delegate { SwitchStateAndPlay(true); };
            _editor.EditorDisabled += delegate { SwitchStateAndPlay(false); };
        }
    }
}
