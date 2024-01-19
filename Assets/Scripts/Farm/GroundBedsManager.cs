using UnityEngine;

public class GroudBedsManager : MonoBehaviour
{
    [SerializeField] private int _groupsCount = 2;
    [SerializeField] private GroundBedsGroup _groundBedGroupPrefab;
    [SerializeField] private GroundBedSettings _bedsSettings;
    private GroundBedsGroup[] _beds;
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
            groundbedGroup.GetComponent<GroundBedsGroup>().BedsSetup(_bedsSettings);
        }
    }

    public float GetBedSize()
    {
        return _groundBedGroupPrefab.GetBedSize();
    }
}
