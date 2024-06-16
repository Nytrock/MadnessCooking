using UnityEngine;

public class NeedHoldAddUI : HoldAddUI {
    [SerializeField] protected CountRenderer _materialCount;

    public override void UpdateCount(HoldAddData data) {
        UpdateCount(data);
        _materialCount.UpdateCount((data as NeedHoldAddData).MaterialCount);
    }
}
