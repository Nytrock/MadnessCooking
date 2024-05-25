using System;
using UnityEngine;

public abstract class SpaceManager<TData> : MonoBehaviour, IUpgradeable, IBindable<TData> where TData: ISaveable
{
    [SerializeField] protected SpacePrefab _spacePrefab;
    [SerializeField] protected int _defaultSpaceCount;
    [SerializeField] protected CountUpgrade[] _spaceAddUpgrades;
    
    protected Transform _spaceContainer;
    protected TData _data;

    public SpaceManagerData SpaceData { get; protected set; }
    public event Action SpaceAdded;

    private void Awake()
    {
        _spaceContainer = transform;
    }

    protected void LateStart()
    {
        GenerateSpaces();
    }

    private void GenerateSpaces()
    {
        for (int i = 0; i < SpaceData.Count; i++)
            AddSpace(i);
    }

    public virtual void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (upgrade == _spaceAddUpgrades[SpaceData.Count - _defaultSpaceCount]) {
            var countUpgrade = upgrade as CountUpgrade;
            SpaceData.Count = countUpgrade.Count;
            AddSpace(SpaceData.Count - 1);
        }
    }

    public virtual void Bind(TData data, bool isFileEmpty)
    {
        _data = data;
        BindData(isFileEmpty);
        LateStart();
    }

    protected void InvokeSpaceAdded()
    {
        SpaceAdded?.Invoke();
    }

    protected abstract void AddSpace(int index);
    protected abstract void BindData(bool isFileEmpty);
}
