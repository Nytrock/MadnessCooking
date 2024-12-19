using UnityEngine;

public abstract class SaveableSpaceManager<TData> : SpaceManager, IBindable<TData>
    where TData : ISaveable {

    [SerializeField] protected UpgradeManager _upgradeManager;
    [SerializeField] protected int _defaultSpaceCount;
    [SerializeField] protected CountUpgrade[] _spaceAddUpgrades;

    protected TData _data;
    protected SpaceManagerData _spaceData;

    public int SpaceCount => _spaceData.Count;

    protected override void Awake() {
        base.Awake();
        _upgradeManager.ItemAdded += CheckSpaceAdded;
    }

    public void LateStart() {
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

    public virtual void Bind(TData data) {
        _data = data;
        BindData();
    }

    public override float GetSpacesSize() {
        return (_spaceData.Count - 1) * SpaceSize;
    }

    protected abstract void BindData();
}
