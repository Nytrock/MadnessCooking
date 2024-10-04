using System;
using UnityEngine;

public class NeedHoldAddUI : HoldAddUI {
    [SerializeField] protected CountRenderer _materialCount;
    private NeedHoldAdd _needHoldAdd;

    protected override void Awake() {
        base.Awake();

        _needHoldAdd = _holdAdd as NeedHoldAdd;
        if (_needHoldAdd == null)
            throw new ArgumentNullException($"Hold add {_holdAdd.name} is not NeedHoldName and cannot be attached to UI {name}");

        _needHoldAdd.MaterialCountChanged += UpdateMaterialCount;
    }

    public void UpdateMaterialCount() {
        _materialCount.UpdateCount(_needHoldAdd.NeedHoldData.MaterialCount);
    }
}
