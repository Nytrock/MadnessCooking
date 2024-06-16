using System.Collections.Generic;
using UnityEngine;

public class FarmBedManager : SpaceManager<FarmData>, IUpgradeable<FarmUpgradeData> {
    [SerializeField] private FarmBedSettings _bedsSettings;
    private readonly List<FarmBedGroup> _beds = new();
    private FarmUpgradeData _upgradeData;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _autoWheatUpgrade;
    [SerializeField] private BaseUpgrade _growStatusShowUpgrade;

    protected override void AddSpace(int index) {
        var farmBedsGroup = Instantiate(_spacePrefab, _spaceContainer) as FarmBedGroup;
        farmBedsGroup.transform.position -= new Vector3(0, _spacePrefab.Size * index, 0);
        farmBedsGroup.BedsSetup(_bedsSettings);
        farmBedsGroup.Bind(_data, _beds.Count);

        _beds.Add(farmBedsGroup);
        InvokeSpaceAdded();
    }

    protected override void BindData(bool isFileEmpty) {
        if (isFileEmpty) {
            _data.GenerateFarmBeds(_spaceAddUpgrades[^1].Count * 3);
            _data.FarmBedGroups = new(_defaultSpaceCount);
        }
        _spaceData = _data.FarmBedGroups;
        _bedsSettings.UIManager.Bind(_data);
    }

    public void BindUpgrade(FarmUpgradeData upgradeData) {
        _upgradeData = upgradeData;
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _growStatusShowUpgrade)
            _upgradeData.ChangeGrowStatusShow();
        else if (upgrade == _autoWheatUpgrade)
            _upgradeData.ChangeAutoWheat();
    }
}
