using UnityEngine;

public class Cow : NeedHoldAdd, IUpgradeable<FarmUpgradeData> {
    [SerializeField] private Puncher _puncher;
    [SerializeField, Min(0)] private float _wastePassiveAmount;
    [SerializeField, Min(0)] private float _wasteActiveAmount;

    private NeedHoldAddData _flourMillData;
    private FarmUpgradeData _upgradeData;

    public int MaterialCount => _needHoldData.MaterialCount;

    protected override void Update() {
        base.Update();
        _puncher.AddWaste(_wastePassiveAmount);
    }

    protected override void AddReady() {
        if (!_upgradeData.IsWheatDistributing)
            _flourMillData.SubstractMaterial();
        _puncher.AddWaste(_wasteActiveAmount);
        base.AddReady();
    }

    public override void Bind(FarmData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.Cow = new();

        _holdData = data.Cow;
        _flourMillData = data.FlourMill;
        base.Bind(data, isFileEmpty);
    }

    public void BindUpgrade(FarmUpgradeData upgradeData) {
        _upgradeData = upgradeData;
    }
}
