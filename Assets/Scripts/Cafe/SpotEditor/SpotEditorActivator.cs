using UnityEngine;

public class SpotEditorActivator : MonoBehaviour {
    [SerializeField] private SpotEditor _spotEditor;
    [SerializeField] private CafeOpener _opener;
    [SerializeField] private Animator _errorAnimator;

    public void OnMouseDown() {
        if (_opener.IsOpened)
            _errorAnimator.SetTrigger("Error");
        else
            _spotEditor.ChangeWorkMode();
    }
}
