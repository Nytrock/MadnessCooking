using System;

public class Puncher : NeedHoldAdd {
    public event Action FertilizeChanged;

    protected override void LateStart() {
        _needHoldData.Unlock();
        base.LateStart();
    }

    public override void SubtractReady() {
        base.SubtractReady();
        FertilizeChanged?.Invoke();
    }

    protected override void AddReady() {
        base.AddReady();
        FertilizeChanged?.Invoke();
    }

    public override void Bind(FarmData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.Puncher = new();
        _holdData = data.Puncher;
        base.Bind(data, isFileEmpty);
    }
}
