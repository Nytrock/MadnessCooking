using System.Collections.Generic;
using UnityEngine;

public class FarmBedManager : SpaceManager<FarmData> {
    [SerializeField] private FarmBedSettings _bedsSettings;
    private readonly List<FarmBedGroup> _beds = new();

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _autoWheatUpgrade;
    [SerializeField] private BaseUpgrade _growStatusShowUpgrade;

    protected override void AddSpace(int index) {
        var farmBedsGroup = Instantiate(_spacePrefab, _spaceContainer) as FarmBedGroup;
        farmBedsGroup.transform.position -= new Vector3(0, SpaceData.SpaceSize * index, 0);
        farmBedsGroup.BedsSetup(_bedsSettings);
        farmBedsGroup.Bind(_data, _beds.Count);

        _beds.Add(farmBedsGroup);
        InvokeSpaceAdded();
    }

    public override void CheckUpgrade(BaseUpgrade upgrade) {
        base.CheckUpgrade(upgrade);
        if (upgrade == _growStatusShowUpgrade)
            _data.IsGrowStatusShow = true;
        else if (upgrade == _autoWheatUpgrade)
            _data.IsAutoWheat = true;
    }

    protected override void BindData(bool isFileEmpty) {
        SpaceData = _data.FarmBedGroups;
        if (isFileEmpty) {
            _data.GenerateFarmBeds(_spaceAddUpgrades[^1].Count * 3);
            SpaceData.Count = _defaultSpaceCount;
        }
        SpaceData.SpaceSize = _spacePrefab.Size;
        _bedsSettings.UIManager.Bind(_data);
    }
}
