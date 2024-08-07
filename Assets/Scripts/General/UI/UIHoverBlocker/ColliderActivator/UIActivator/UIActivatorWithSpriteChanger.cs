using UnityEngine;

public class UIActivatorWithSpriteChanger : UIActivator {
    [SerializeField] private SpriteChanger _changer;

    private void Start() {
        _changer.ChangeSpriteState(false);
    }

    protected override void Press() {
        base.Press();
        _changer.ChangeSpriteState();
    }

    protected override void CloseUI() {
        base.CloseUI();
        _changer.ChangeSpriteState(false);
    }
}
