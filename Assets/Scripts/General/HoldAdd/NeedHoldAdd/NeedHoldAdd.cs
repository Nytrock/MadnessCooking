using System;

public abstract class NeedHoldAdd : HoldAdd
{
    protected NeedHoldAddUI _needHoldUI => _holdUI as NeedHoldAddUI;

    public NeedHoldAddData NeedHoldData => HoldData as NeedHoldAddData;

    protected override void LateStart()
    {
        if (NeedHoldData == null || _needHoldUI == null)
            throw new ArgumentNullException("Argument for data or for UI are null");
        base.LateStart();
    }

    protected override void UpdateTimer()
    {
        if (NeedHoldData.MaterialCount == 0)
            return;

        base.UpdateTimer();
    }

    public override void ChangeWorkMode(bool newValue)
    {
        if (NeedHoldData.MaterialCount == 0) {
            _needHoldUI.ChangeUI(newValue);
            return;
        }

        base.ChangeWorkMode(newValue);
    }

    protected override void Add()
    {
        NeedHoldData.MaterialCount--;
        base.Add();

        if (NeedHoldData.MaterialCount == 0)
            _isWork = false;
    }
}
