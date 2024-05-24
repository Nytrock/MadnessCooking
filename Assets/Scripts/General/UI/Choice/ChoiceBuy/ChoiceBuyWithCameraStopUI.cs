using UnityEngine;

public abstract class ChoiceBuyWithCameraStopUI<TItem, TData> : ChoiceBuyUI<TItem> where TItem : BuyableObject where TData: ISaveable
{
    [SerializeField] protected CameraManager<TData> _cameraManager;

    protected override void Activate()
    {
        base.Activate();
        _cameraManager.ChangeWorkMode(false);
    }

    public override void Disable()
    {
        base.Disable();
        _cameraManager.ChangeWorkMode(true);
    }
}
