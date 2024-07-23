using System;

public abstract class NeedHoldAdd : HoldAdd {
    protected NeedHoldAddUI _needHoldUI => _holdUI as NeedHoldAddUI;
    protected NeedHoldAddData _needHoldData => _data as NeedHoldAddData;

    protected override void LateStart() {
        if (_needHoldData == null || _needHoldUI == null)
            throw new ArgumentNullException("Argument for data or for UI are null");
        base.LateStart();
    }

    protected override void UpdateTimer() {
        if (_needHoldData.MaterialCount == 0)
            return;

        base.UpdateTimer();
    }

    public override void ChangeWorkMode(bool newValue) {
        if (_needHoldData.MaterialCount == 0) {
            _needHoldUI.ChangeUI(newValue);
            return;
        }

        base.ChangeWorkMode(newValue);
    }

    protected override void AddReady() {
        _needHoldData.SubstractMaterial();
        base.AddReady();

        if (_needHoldData.MaterialCount == 0)
            _isWork = false;
    }

    public void AddMaterial(int count) {
        _needHoldData.AddMaterial(count);
    }

    public void AddMaterial() {
        _needHoldData.AddMaterial();
    }

    public void ClearMaterials() {
        _needHoldData.SetMaterial(0);
    }
}
