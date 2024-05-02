using UnityEngine;

public abstract class ChoiceBuyWithCameraStopUI<T, K> : ChoiceBuyUI<T> where T : BuyableObject where K: ISaveable
{
    [SerializeField] protected CameraManager<K> _cameraManager;

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
