using UnityEngine;

public class UIActivatorWithVisualChanger : UIActivator {
    [SerializeField] private VisualChanger _changer;

    private void Start() {
        _changer.ChangeState(false);
    }

    protected override void Press() {
        base.Press();
        _changer.ChangeState();
    }

    protected override void CloseUI() {
        base.CloseUI();
        _changer.ChangeState(false);
    }
}
