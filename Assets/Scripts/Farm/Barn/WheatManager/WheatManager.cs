using System;
using UnityEngine;

public class WheatManager : MonoBehaviour, IUpgradeable<FarmUpgradeData>, IBindable<FarmData> {
    [SerializeField] private Cow _cow;
    [SerializeField] private FlourMill _flourMill;
    [SerializeField] private BaseUpgrade _wheatDistributeUpgrade;

    private WheatManagerData _data;
    private FarmUpgradeData _upgradeData;

    public void AddWheat(int count) {
        if (_upgradeData.IsWheatDistributing) {
            DistributeWheat(count);
        } else {
            _cow.AddMaterial(count);
            _flourMill.AddMaterial(count);
        }
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _wheatDistributeUpgrade) {
            _upgradeData.ChangeWheatDistributing();
            int count = _cow.MaterialCount;
            _cow.ClearMaterials();
            _flourMill.ClearMaterials();
            DistributeWheat(count);
        }
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

    public void Bind(FarmData data) {
        data.WheatManager ??= new();
        _data = data.WheatManager;
    }

    public void BindUpgrade(FarmUpgradeData upgradeData) {
        _upgradeData = upgradeData;
    }
}
