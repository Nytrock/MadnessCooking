using System;
using System.Linq;
using UnityEngine;

public class CafeSpaceManager : MonoBehaviour, IUpgradeable
{
    [SerializeField] private CafeSpace _spacePrefab;

    [Header("Upgrades")]
    [SerializeField] private CountUpgrade[] _upgrades;
    
    private Transform _spaceContainer;
    private int _spaceCount = 3;

    public int SpaceCount => _spaceCount;
    public event Action SpaceAdded;

    private void Start()
    {
        _spaceContainer = transform;
        GenerateSpaces();
    }

    private void GenerateSpaces()
    {
        var size = _spacePrefab.GetSpaceSize();
        for (int i = 0; i < _spaceCount; i++) {
            var space = Instantiate(_spacePrefab);
            space.transform.parent = _spaceContainer;
            space.transform.position += new Vector3(size * i, 0, 0);
        }
    }

    public float GetSpaceSize()
    {
        return _spacePrefab.GetSpaceSize();
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (_upgrades.Contains(upgrade)) {
            var countUpgrade = upgrade as CountUpgrade;
            _spaceCount = countUpgrade.Count;
            Debug.Log(_spaceCount);
            AddSpace();
        }
    }
    
    private void AddSpace()
    {
        var space = Instantiate(_spacePrefab);
        space.transform.parent = _spaceContainer;
        space.transform.position += new Vector3(
            _spacePrefab.GetSpaceSize() * (_spaceCount - 1), 0, 0
        );
        SpaceAdded?.Invoke();
    }
}
