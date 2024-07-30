using System;
using UnityEngine;

public class FarmWell : HoldAdd {
    [SerializeField] private VerticalCameraManager _cameraManager;

    public event Action WaterChanged;

    protected override void AddReady() {
        base.AddReady();
        WaterChanged?.Invoke();
    }

    public override void ChangeWorkMode(bool newValue) {
        base.ChangeWorkMode(newValue);
        _cameraManager.ChangeWorkMode(!_isWork);
    }

    public override void SubtractReady() {
        base.SubtractReady();
        WaterChanged?.Invoke();
    }

    public override void Bind(FarmData data) {
        data.FarmWell ??= new();
        _data = data.FarmWell;
        base.Bind(data);
    }
}
