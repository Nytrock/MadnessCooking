using System;
using UnityEngine;

public class FarmWell : HoldAdd
{
    [SerializeField] private FarmCameraManager _cameraManager;

    public event Action<int> WaterChanged;

    protected override void Start()
    {
        _UI.SetCountText(_readyCount);
        base.Start();
    }

    protected override void Add()
    {
        base.Add();
        WaterChanged?.Invoke(_readyCount);
    }

    public override void ChangeWorkMode(bool newValue)
    {
        base.ChangeWorkMode(newValue);
        _cameraManager.ChangeWorkMode(!_isWork);
    }

    public void SubtractWater()
    {
        _readyCount--;
        _UI.SetCountText(_readyCount);
        WaterChanged?.Invoke(_readyCount);
    }
}
