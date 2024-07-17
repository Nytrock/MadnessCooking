using UnityEngine;

public class SpotEditorActivator : MonoBehaviour {
    [SerializeField] private ErrorMessageRenderer _errorRenderer;
    [SerializeField] private SpotEditor _spotEditor;
    [SerializeField] private CafeStateChanger _opener;

    public void OnMouseDown() {
        if (FatigueManager.Instance.IsTired)
            return;

        if (_opener.IsOpened)
            _errorRenderer.ShowError();
        else
            _spotEditor.ChangeWorkMode();
    }
}