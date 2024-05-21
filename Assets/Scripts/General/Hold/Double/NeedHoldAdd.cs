using UnityEngine;

public abstract class NeedHoldAdd : HoldAdd
{
    protected NeedHoldAddUI _needHoldUI => _holdUI as NeedHoldAddUI;

    public SerializableNeedHoldAdd NeedHoldData => HoldData as SerializableNeedHoldAdd;

    protected override void LateStart()
    {
        if (NeedHoldData == null || _needHoldUI == null)
            Debug.LogError("Class mismatch");
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
