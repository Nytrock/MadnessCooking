using UnityEngine;

public class FarmBedGroup : MonoBehaviour
{
    [SerializeField] private float _size;
    [SerializeField] private FarmBed[] _groundBeds;

    public float GetBedSize()
    {
        return _size;
    }

    public void BedsSetup(FarmBedSettings settings)
    {
        foreach (var bed in _groundBeds) {
            bed.Setup(settings);
            bed.GetComponent<BedChoice>().Setup(settings);
        }
    }
}
