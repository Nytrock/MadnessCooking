using System;
using UnityEngine;

public class FarmWell : HoldAdd
{
    [SerializeField] private FarmCameraManager _cameraManager;

    public event Action<int> WaterAdded;

    protected override void Start()
    {
        _UI.SetCountText(_count);
        base.Start();
    }

    protected override void Add()
    {
        base.Add();
        WaterAdded?.Invoke(_count);
    }

    public override void ChangeWorkMode(bool newValue)
    {
        base.ChangeWorkMode(newValue);
        _cameraManager.ChangeWorkMode(!_isWork);
    }

    public void SubtractWater()
    {
        _count--;
        _UI.SetCountText(_count);
    }
}
