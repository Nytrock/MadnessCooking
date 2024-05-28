using UnityEngine;

public class NeedHoldAddUI : HoldAddUI {
    [SerializeField] protected CountRenderer _materialCount;

    protected NeedHoldAdd _needHoldAdd => _holdAdd as NeedHoldAdd;

    protected override void Update() {
        base.Update();
        _materialCount.UpdateCount(_needHoldAdd.NeedHoldData.MaterialCount);
    }
}
