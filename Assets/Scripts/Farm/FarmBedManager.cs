using System.Collections.Generic;
using UnityEngine;

public class FarmBedManager : SpaceManager
{
    [SerializeField] private FarmBedSettings _bedsSettings;
    private List<FarmBedGroup> _beds = new();

    protected override void AddSpace(float size, int index)
    {
        var groundbedGroup = Instantiate(_spacePrefab, _spaceContainer) as FarmBedGroup;
        groundbedGroup.transform.position -= new Vector3(0, size * index, 0);
        groundbedGroup.BedsSetup(_bedsSettings);
        _beds.Add(groundbedGroup);
    }

    public override void CheckUpgrade(BaseUpgrade upgrade)
    {
        base.CheckUpgrade(upgrade);
        foreach (var group in _beds)
            group.CheckUpgrade(upgrade);
    }
}
