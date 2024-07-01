using UnityEngine;

public abstract class ChoiceSimpleWithCameraStopUI<TItem> : ChoiceSimpleUI<TItem>
    where TItem : BuyableItem {

    [SerializeField] protected CameraManager _cameraManager;

    protected override void Activate() {
        base.Activate();
        _cameraManager.ChangeWorkMode(false);
    }

    public override void Disable() {
        base.Disable();
        _cameraManager.ChangeWorkMode(true);
    }
}
