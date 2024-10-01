using System;
using UnityEngine;

public abstract class NeedHoldAdd : HoldAdd {
    [SerializeField, Min(0)] protected int _materialDefaultCount;

    public NeedHoldAddData NeedHoldData => Data as NeedHoldAddData;

    protected override void LateStart() {
        if (NeedHoldData == null)
            throw new ArgumentNullException("Argument for data or for UI are null");
        base.LateStart();
    }

    protected override void UpdateTimer() {
        if (NeedHoldData.MaterialCount == 0)
            return;

        base.UpdateTimer();
    }

    public override void ChangeClickMode(bool newValue) {
        if (NeedHoldData.MaterialCount == 0) {
            InvokeClickChanged(newValue);
            return;
        }

        base.ChangeClickMode(newValue);
    }

    protected override void AddReady() {
        NeedHoldData.SubstractMaterial();
        base.AddReady();

        if (NeedHoldData.MaterialCount == 0)
            InvokeWorkChanged(false);
    }

    public void AddMaterial(int count) {
        NeedHoldData.AddMaterial(count);
    }

    public void AddMaterial() {
        NeedHoldData.AddMaterial();
    }

    public void ClearMaterials() {
        NeedHoldData.SetMaterial(0);
    }
}
