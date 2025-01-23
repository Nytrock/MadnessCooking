using System;
using UnityEngine;

public abstract class HoldAdd : MonoBehaviour, IBindable<FarmData> {
    [SerializeField] private UpgradeManager _upgradeManager;

    [Header("Upgrades")]
    [SerializeField] protected BaseUpgrade _unlockUpgrade;
    [SerializeField] protected CoefficientUpgrade _autoWorkUpgrade;

    [Header("Main")]
    [SerializeField, Min(0)] protected float _timeWait;
    [SerializeField, Min(0)] private float _fatigueCoef;
    [SerializeField, Min(0)] protected int _readyDefaultCount;
    [SerializeField] protected VisualChanger _unlockVisual;

    public HoldAddData Data { get; protected set; }

    public bool IsUnlocked => Data.IsUnlocked;

    public event Action<bool> ClickChanged;
    public event Action<bool> WorkChanged;
    public event Action ReadyCountChanged;
    public event Action SetupEnded;

    protected virtual void Awake() {
        _upgradeManager.ItemAdded += CheckAddedUpgrade;
    }

    public virtual void LateStart() {
        ResetAll();
        UpdateUpgrades();
        Data.SetTimeWait(_timeWait);

        InvokeClickChanged(false);
        SetupEnded?.Invoke();
        ReadyCountChanged?.Invoke();
    }

    private void ResetAll() {
        Data.ResetAll();
    }

    protected virtual void UpdateUpgrades() {
        _unlockVisual.ChangeState(Data.IsUnlocked);
    }

    public virtual void ChangeClickMode(bool newValue) {
        if (Data.IsAuto) {
            InvokeClickChanged(newValue);
            return;
        }

        Data.ChangeWork(newValue);
        InvokeClickChanged(newValue);
        InvokeWorkChanged(newValue);
    }

    protected virtual void Update() {
        if (!Data.IsWork && !Data.IsAuto)
            return;

        UpdateTimer();
    }

    protected virtual void UpdateTimer() {
        if (!Data.IsAuto)
            FatigueManager.Instance.ChangeFatigue(_fatigueCoef * FpsManager.NORMALIZED_DELTA_TIME);

        if (Data.NowTime < _timeWait)
            Data.UpdateTime();
        else
            AddReady();
    }

    protected virtual void AddReady() {
        Data.AddReady();
        ReadyCountChanged?.Invoke();
    }

    public void SetReady(int count) {
        Data.SetReady(count);
        ReadyCountChanged?.Invoke();
    }

    public virtual void SubtractReady() {
        Data.SubtractReady();
        ReadyCountChanged?.Invoke();
    }

    public virtual void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _unlockUpgrade) {
            Data.Unlock();
        } else if (upgrade == _autoWorkUpgrade) {
            Data.MakeAuto(_autoWorkUpgrade);
            InvokeWorkChanged(true);
        }

        UpdateUpgrades();
    }

    public abstract void Bind(FarmData data);

    protected void InvokeClickChanged(bool isWork) {
        ClickChanged?.Invoke(isWork);
    }

    protected void InvokeWorkChanged(bool isWork) {
        WorkChanged?.Invoke(isWork);
    }
}
