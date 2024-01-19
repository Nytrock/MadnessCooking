using UnityEngine;

public class GroundBedsGroup : MonoBehaviour
{
    [SerializeField] private float _size;
    [SerializeField] private GroundBed[] _groundBeds;

    public float GetBedSize()
    {
        return _size;
    }

    public void BedsSetup(GroundBedSettings settings)
    {
        foreach (var bed in _groundBeds) {
            bed.Setup(settings);
            bed.GetComponent<BedChoice>().Setup(settings);
        }
    }
}
