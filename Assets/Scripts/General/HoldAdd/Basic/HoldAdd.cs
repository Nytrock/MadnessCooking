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
    protected HoldAddData _holdData;

    public int ReadyCount => _holdData.ReadyCount;

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
        _holdData.ResetAll();
    }

    private void UpdateUpgrades() {
        gameObject.SetActive(_holdData.IsUnlocked);
        _holdData.UpdateUpgrades(_autoWorkUpgrade);
    }

    public virtual void ChangeWorkMode(bool newValue) {
        _holdUI.ChangeUI(newValue);
        if (_holdData.IsAuto)
            return;

        _isWork = newValue;
        if (!_isWork)
            _holdData.ResetTime();
    }

    private void Update() {
        if (!_isWork && !_holdData.IsAuto)
            return;

        UpdateTimer();
    }

    protected virtual void UpdateTimer() {
        if (!_holdData.IsAuto)
            FatigueManager.Instance.ChangeFatigue(_fatigueCoef);

        if (_holdData.NowTime < _timeWait) {
            _holdData.UpdateTime();
            _holdUI.UpdateTime(_holdData.NowTime);
        } else {
            AddReady();
        }
    }

    protected virtual void AddReady() {
        _holdData.AddReady();
        _holdUI.UpdateCount(_holdData);
    }

    public void SetReady(int count) {
        _holdData.SetReady(count);
        _holdUI.UpdateCount(_holdData);
    }

    public virtual void SubtractReady() {
        _holdData.SubtractReady();
        _holdUI.UpdateCount(_holdData);
    }

    public virtual void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _unlockUpgrade)
            _holdData.Unlock();
        else if (upgrade == _autoWorkUpgrade)
            _holdData.MakeAuto();

        UpdateUpgrades();
    }

    public virtual void Bind(FarmData data, bool isFileEmpty) {
        LateStart();
    }
}
