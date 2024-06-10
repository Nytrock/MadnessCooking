using UnityEngine;

public class SpotEditorActivator : MonoBehaviour {
    [SerializeField] private ErrorMessageRenderer _errorRenderer;
    [SerializeField] private SpotEditor _spotEditor;
    [SerializeField] private CafeOpener _opener;

    public void OnMouseDown() {
        if (_opener.IsOpened)
            _errorRenderer.ShowError();
        else
            _spotEditor.ChangeWorkMode();
    }
}