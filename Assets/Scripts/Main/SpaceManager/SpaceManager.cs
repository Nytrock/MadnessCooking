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
    public float SpaceSize => _spacePrefab.Size;

    public event Action SpaceAdded;

    private void Awake()
    {
        _spaceCount = _defaultSpaceCount;
        _spaceContainer = transform;
    }

    protected void LateStart()
    {
        GenerateSpaces();
    }

    private void GenerateSpaces()
    {
        var size = _spacePrefab.Size;
        for (int i = 0; i < _spaceCount; i++)
            AddSpace(size, i);
    }

    public virtual void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (_spaceAddUpgrades.Contains(upgrade)) {
            var countUpgrade = upgrade as CountUpgrade;
            AddSpace(_spacePrefab.Size, _spaceCount);
            _spaceCount = countUpgrade.Count;
            UpdateData();
            SpaceAdded?.Invoke();
        }
    }

    protected abstract void AddSpace(float size, int index);
    protected abstract void UpdateData();
}
