using UnityEngine;

public class FarmBedGroup : SpacePrefab
{
    [SerializeField] private BedChoice[] _farmBeds = new BedChoice[FARM_BEDS_COUNT];
    private const int FARM_BEDS_COUNT = 3;

    public void BedsSetup(FarmBedSettings settings)
    {
        foreach (var bed in _farmBeds)
            bed.Setup(settings);
    }

    public void Bind(FarmData data, int groupIndex)
    {
        for (int i = 0; i < FARM_BEDS_COUNT; i++)
            _farmBeds[i].Bind(data, (groupIndex * 3) + i);
    }
}
