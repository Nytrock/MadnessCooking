using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    public class SpotEditorActivator : ColliderActivator {
        [SerializeField] private ErrorMessageRenderer _errorRenderer;
        [SerializeField] private SpotEditor _spotEditor;
        [SerializeField] private CafeStateChanger _opener;

        protected override void Press() {
            if (_opener.IsOpened)
                _errorRenderer.ShowError();
            else
                _spotEditor.ChangeWorkMode();
        }
    }
}