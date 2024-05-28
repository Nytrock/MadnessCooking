using System;

public class Puncher : NeedHoldAdd {
    public event Action FertilizeChanged;

    protected override void LateStart() {
        NeedHoldData.IsUnlocked = true;
        base.LateStart();
    }

    public void AddShit() {
        NeedHoldData.MaterialCount++;
    }

    public void SubtractFertilize() {
        HoldData.ReadyCount--;
        FertilizeChanged?.Invoke();
    }

    protected override void Add() {
        base.Add();
        FertilizeChanged?.Invoke();
    }

    public override void Bind(FarmData data, bool isFileEmpty) {
        HoldData = data.Puncher;
        base.Bind(data, isFileEmpty);
    }
}
