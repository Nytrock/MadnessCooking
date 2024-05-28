using UnityEngine;

public abstract class ChoiceSimpleWithCameraStopUI<TItem, TData> : ChoiceSimpleUI<TItem> where TItem : BuyableObject where TData : ISaveable {
    [SerializeField] protected CameraManager<TData> _cameraManager;

    protected override void Activate() {
        base.Activate();
        _cameraManager.ChangeWorkMode(false);
    }

    public override void Disable() {
        base.Disable();
        _cameraManager.ChangeWorkMode(true);
    }
}
