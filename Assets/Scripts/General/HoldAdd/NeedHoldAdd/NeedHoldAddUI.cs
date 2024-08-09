using UnityEngine;

public class NeedHoldAddUI : HoldAddUI {
    [SerializeField] protected CountRenderer _materialCount;

    public override void UpdateCount() {
        base.UpdateCount();
        _materialCount.UpdateCount((_holdAdd.Data as NeedHoldAddData).MaterialCount);
    }
}
