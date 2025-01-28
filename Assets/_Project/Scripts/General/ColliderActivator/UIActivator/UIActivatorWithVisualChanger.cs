using UnityEngine;

public class UIActivatorWithVisualChanger : UIActivator {
    [SerializeField] private VisualChanger _changer;

    private void Awake() {
        _activableObject.Value.StateChanged += _changer.ChangeState;
    }
}
