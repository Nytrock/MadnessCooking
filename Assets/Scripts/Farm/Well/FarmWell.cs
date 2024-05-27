using System;
using UnityEngine;

public class FarmWell : HoldAdd
{
    [SerializeField] private FarmCameraManager _cameraManager;

    public event Action WaterChanged;

    protected override void Add()
    {
        base.Add();
        WaterChanged?.Invoke();
    }

    public override void ChangeWorkMode(bool newValue)
    {
        base.ChangeWorkMode(newValue);
        _cameraManager.ChangeWorkMode(!_isWork);
    }

    public void SubtractWater()
    {
        HoldData.ReadyCount--;
        WaterChanged?.Invoke();
    }

    public override void Bind(FarmData data, bool isFileEmpty)
    {
        HoldData = data.FarmWell;
        base.Bind(data, isFileEmpty);
    }
}
