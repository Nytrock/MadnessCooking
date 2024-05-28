using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FlourMill : NeedHoldAdd {
    private NeedHoldAddData _cowData;
    private FarmData _data;

    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    protected override void Add() {
        if (!_data.IsWheatDistributing)
            _cowData.MaterialCount--;
        base.Add();
    }

    public override void ChangeWorkMode(bool newValue) {
        _animator.SetBool("isHold", newValue && NeedHoldData.MaterialCount > 0);
        base.ChangeWorkMode(newValue);
    }

    public override void Bind(FarmData data, bool isFileEmpty) {
        HoldData = data.FlourMill;
        _cowData = data.Cow;
        _data = data;
        base.Bind(data, isFileEmpty);
    }
}
