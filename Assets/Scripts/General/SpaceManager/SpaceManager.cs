using System;
using UnityEngine;

public abstract class SpaceManager<TData> : MonoBehaviour, IBindable<TData>
    where TData : ISaveable {

    [SerializeField] protected SpacePrefab _spacePrefab;
    [SerializeField] protected UpgradeManager _upgradeManager;
    [SerializeField] protected int _defaultSpaceCount;
    [SerializeField] protected CountUpgrade[] _spaceAddUpgrades;

    protected Transform _spaceContainer;
    protected TData _data;
    protected SpaceManagerData _spaceData;

    public event Action SpaceAdded;

    public float SpaceSize => _spacePrefab.Size;
    public int SpaceCount => _spaceData.Count;

    private void Awake() {
        _spaceContainer = transform;
        _upgradeManager.ItemAdded += CheckSpaceAdded;
    }

    protected void LateStart() {
        GenerateSpaces();
    }

    private void GenerateSpaces() {
        for (int i = 0; i < _spaceData.Count; i++)
            AddSpace(i);
    }

    public virtual void CheckSpaceAdded(BaseUpgrade upgrade) {
        if (upgrade == _spaceAddUpgrades[_spaceData.Count - _defaultSpaceCount]) {
            _spaceData.SetCount(upgrade as CountUpgrade);
            AddSpace(_spaceData.Count - 1);
        }
    }

    public virtual void Bind(TData data, bool isFileEmpty) {
        _data = data;
        BindData(isFileEmpty);
        LateStart();
    }

    protected void InvokeSpaceAdded() {
        SpaceAdded?.Invoke();
    }

    protected abstract void AddSpace(int index);
    protected abstract void BindData(bool isFileEmpty);
}
