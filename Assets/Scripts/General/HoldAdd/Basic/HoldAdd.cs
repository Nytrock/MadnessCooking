using UnityEngine;

public abstract class HoldAdd : MonoBehaviour, IBindable<FarmData> {
    [SerializeField] private UpgradeManager _upgradeManager;

    [Header("Upgrades")]
    [SerializeField] protected BaseUpgrade _unlockUpgrade;
    [SerializeField] protected CoefficientUpgrade _autoWorkUpgrade;

    [Header("Main")]
    [SerializeField] protected HoldAddUI _holdUI;
    [SerializeField, Min(0)] private float _timeWait;
    [SerializeField, Min(0)] private float _fatigueCoef;

    protected bool _isWork;
    protected HoldAddData _data;

    public int ReadyCount => _data.ReadyCount;

    private void Awake() {
        _upgradeManager.ItemAdded += CheckAddedUpgrade;
    }

    protected virtual void LateStart() {
        ResetAll();
        UpdateUpgrades();
    }

    private void ResetAll() {
        _isWork = false;
        _holdUI.SetTimeWait(_timeWait);
        _holdUI.ChangeUI(_isWork);
        _data.ResetAll();
    }

    protected virtual void UpdateUpgrades() {
        gameObject.SetActive(_data.IsUnlocked);
        _data.UpdateUpgrades(_autoWorkUpgrade);
    }

    public virtual void ChangeWorkMode(bool newValue) {
        _holdUI.ChangeUI(newValue);
        if (_data.IsAuto)
            return;

        _isWork = newValue;
        if (!_isWork)
            _data.ResetTime();
    }

    protected virtual void Update() {
        if (!_isWork && !_data.IsAuto)
            return;

        UpdateTimer();
    }

    protected virtual void UpdateTimer() {
        if (!_data.IsAuto)
            FatigueManager.Instance.ChangeFatigue(_fatigueCoef);

        if (_data.NowTime < _timeWait) {
            _data.UpdateTime();
            _holdUI.UpdateTime(_data.NowTime);
        } else {
            AddReady();
        }
    }

    protected virtual void AddReady() {
        _data.AddReady();
        _holdUI.UpdateCount(_data);
    }

    public void SetReady(int count) {
        _data.SetReady(count);
        _holdUI.UpdateCount(_data);
    }

    public virtual void SubtractReady() {
        _data.SubtractReady();
        _holdUI.UpdateCount(_data);
    }

    public virtual void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _unlockUpgrade)
            _data.Unlock();
        else if (upgrade == _autoWorkUpgrade)
            _data.MakeAuto();

        UpdateUpgrades();
    }

    public virtual void Bind(FarmData data) {
        LateStart();
    }
}
