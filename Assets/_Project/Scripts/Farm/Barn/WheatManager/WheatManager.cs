using System;
using UnityEngine;

public class WheatManager : MonoBehaviour, IBindable<FarmData> {
    [SerializeField] private Cow _cow;
    [SerializeField] private FlourMill _flourMill;

    [Header("Upgrades")]
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private BaseUpgrade _wheatDistributeUpgrade;
    private bool _isWheatDistributing = false;

    private WheatManagerData _data;

    private void Awake() {
        _upgradeManager.ItemAdded += CheckAddedUpgrade;
        _cow.ReadyCountChanged += MakeWheatSame;
        _flourMill.ReadyCountChanged += MakeWheatSame;
    }

    [ContextMenu("AddWheat")]
    private void TestAddWheat() {
        if (!Application.isPlaying)
            return;

        AddWheat(2);
    }

    private void MakeWheatSame() {
        if (_flourMill.NeedHoldData == null || _cow.NeedHoldData == null)
            return;

        if (_isWheatDistributing)
            return;

        int cowWheatCount = _cow.NeedHoldData.MaterialCount;
        int flourMillWheatCount = _flourMill.NeedHoldData.MaterialCount;
        if (cowWheatCount == flourMillWheatCount)
            return;

        int wheatCount = Mathf.Min(cowWheatCount, flourMillWheatCount);
        _cow.SetMaterial(wheatCount);
        _flourMill.SetMaterial(wheatCount);
    }

    public void AddWheat(int count) {
        if (_isWheatDistributing) {
            DistributeWheat(count);
        } else {
            _cow.AddMaterial(count);
            _flourMill.AddMaterial(count);
        }
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _wheatDistributeUpgrade) {
            _isWheatDistributing = true;

            if (!_data.IsWheatDistributed)
                DistributeWheatAfterUpgrade();
        }
    }

    private void DistributeWheatAfterUpgrade() {
        int count = _cow.NeedHoldData.MaterialCount;
        _cow.ClearMaterials();
        _flourMill.ClearMaterials();
        DistributeWheat(count);

        _data.DistributeWheat();
    }

    private void DistributeWheat(int count) {
        int halfCount = count / 2;

        _cow.AddMaterial(halfCount);
        _flourMill.AddMaterial(halfCount);

        if (count % 2 != 0) {
            _cow.AddMaterial(Convert.ToInt32(_data.IsCowNextWheat));
            _flourMill.AddMaterial(Convert.ToInt32(!_data.IsCowNextWheat));
            _data.ChangeCowNextWheat();
        }
    }

    public void LateStart() { }

    public void Bind(FarmData data) {
        data.WheatManager ??= new();
        _data = data.WheatManager;
    }
}
