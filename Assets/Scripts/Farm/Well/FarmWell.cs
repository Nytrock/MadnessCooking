using System;
using UnityEngine;

public class FarmWell : HoldAdd {
    [SerializeField] private FarmCameraManager _cameraManager;

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

    public override void Bind(FarmData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.FarmWell = new();
        _holdData = data.FarmWell;
        base.Bind(data, isFileEmpty);
    }
}
