using UnityEngine;

public class FarmBedGroup : SpacePrefab
{
    [SerializeField] private FarmBed[] _groundBeds;

    public void BedsSetup(FarmBedSettings settings)
    {
        foreach (var bed in _groundBeds) {
            bed.Setup(settings);
            bed.GetComponent<BedChoice>().Setup(settings);
        }
    }
}
