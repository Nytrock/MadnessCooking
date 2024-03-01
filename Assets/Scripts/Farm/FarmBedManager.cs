using UnityEngine;

public class FarmBedManager : MonoBehaviour
{
    [SerializeField] private int _groupsCount = 2;
    [SerializeField] private FarmBedGroup _groundBedGroupPrefab;
    [SerializeField] private FarmBedSettings _bedsSettings;
    private FarmBedGroup[] _beds;
    private Transform _groupsContainer;

    public int GroupsCount => _groupsCount;

    private void Start()
    {
        _groupsContainer = transform;
        SetGroups();
    }

    private void SetGroups()
    {
        var size = _groundBedGroupPrefab.GetBedSize();

        for (int i = 0; i < _groupsCount; i++) {
            var groundbedGroup = Instantiate(_groundBedGroupPrefab, _groupsContainer);
            groundbedGroup.transform.position -= new Vector3(0, size * i, 0);
            groundbedGroup.GetComponent<FarmBedGroup>().BedsSetup(_bedsSettings);
        }
    }

    public float GetBedSize()
    {
        return _groundBedGroupPrefab.GetBedSize();
    }
}
