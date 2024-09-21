using UnityEngine;

public class UIActivatorWithSpriteChanger : UIActivator {
    [SerializeField] private SpriteChanger _changer;

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
