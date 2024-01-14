using UnityEngine;

public class SpotEditorActivator : MonoBehaviour { 
    [SerializeField] private SpotEditor _spotEditor;
    [SerializeField] private CafeOpener _opener;
    [SerializeField] private Animator _textErrorAnimator;

    public void OnMouseDown()
    {
        if (_opener.IsOpened)
            _textErrorAnimator.SetTrigger("Error");
        else
            _spotEditor.ChangeWorkMode();
    }
}
