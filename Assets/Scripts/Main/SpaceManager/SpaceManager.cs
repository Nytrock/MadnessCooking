using System;
using System.Linq;
using UnityEngine;

public abstract class SpaceManager : MonoBehaviour, IUpgradeable
{
    [SerializeField] protected SpacePrefab _spacePrefab;
    [SerializeField] protected int _defaultSpaceCount;
    [SerializeField] protected CountUpgrade[] _spaceAddUpgrades;
    protected int _spaceCount;
    protected Transform _spaceContainer;

    public int SpaceCount => _spaceCount;
    public event Action SpaceAdded;

    private void Awake()
    {
        _spaceCount = _defaultSpaceCount;
    }

    private void Start()
    {
        _spaceContainer = transform;
        GenerateSpaces();
    }

    private void GenerateSpaces()
    {
        var size = _spacePrefab.GetSpaceSize();
        for (int i = 0; i < _spaceCount; i++)
            AddSpace(size, i);
    }

    public float GetSpaceSize()
    {
        return _spacePrefab.GetSpaceSize();
    }

    public virtual void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (_spaceAddUpgrades.Contains(upgrade)) {
            var countUpgrade = upgrade as CountUpgrade;
            AddSpace(_spacePrefab.GetSpaceSize(), _spaceCount);
            _spaceCount = countUpgrade.Count;
            SpaceAdded?.Invoke();
        }
    }

    protected abstract void AddSpace(float size, int index);
}
