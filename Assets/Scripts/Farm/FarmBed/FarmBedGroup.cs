using UnityEngine;

public class FarmBedGroup : SpacePrefab
{
    [SerializeField] private BedChoice[] _farmBeds = new BedChoice[_farmBedsCount];
    private const int _farmBedsCount = 3;

    public void BedsSetup(FarmBedSettings settings)
    {
        foreach (var bed in _farmBeds)
            bed.Setup(settings);
    }

    public void Bind(FarmData data, int groupIndex)
    {
        for (int i = 0; i < _farmBedsCount; i++)
            _farmBeds[i].Bind(data, groupIndex * 3 + i);
    }
}
