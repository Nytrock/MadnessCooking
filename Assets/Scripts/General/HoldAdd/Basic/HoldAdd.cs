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

    public HoldAddData Data { get; protected set; }

    public event Action<bool> WorkChanged;
    public event Action CountChanged;
    public event Action SetupEnded;

    protected virtual void Awake() {
        _upgradeManager.ItemAdded += CheckAddedUpgrade;
    }

    protected virtual void LateStart() {
        ResetAll();
        UpdateUpgrades();
        Data.SetTimeWait(_timeWait);

        WorkChanged?.Invoke(false);
        SetupEnded?.Invoke();
        CountChanged?.Invoke();
    }

    private void ResetAll() {
        Data.ResetAll();
    }

    protected virtual void UpdateUpgrades() {
        gameObject.SetActive(Data.IsUnlocked);
        Data.UpdateUpgrades(_autoWorkUpgrade);
    }

    public virtual void ChangeWorkMode(bool newValue) {
        if (Data.IsAuto) {
            InvokeWorkChanged(newValue);
            return;
        }

        Data.ChangeWork(newValue);
        InvokeWorkChanged(Data.IsWork);
    }

    protected virtual void Update() {
        if (!Data.IsWork && !Data.IsAuto)
            return;

        UpdateTimer();
    }

    protected virtual void UpdateTimer() {
        if (!Data.IsAuto)
            FatigueManager.Instance.ChangeFatigue(_fatigueCoef);

        if (Data.NowTime < _timeWait)
            Data.UpdateTime();
        else
            AddReady();
    }

    protected virtual void AddReady() {
        Data.AddReady();
        CountChanged?.Invoke();
    }

    public void SetReady(int count) {
        Data.SetReady(count);
        CountChanged?.Invoke();
    }

    public virtual void SubtractReady() {
        Data.SubtractReady();
        CountChanged?.Invoke();
    }

    public virtual void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _unlockUpgrade)
            Data.Unlock();
        else if (upgrade == _autoWorkUpgrade)
            Data.MakeAuto();

        UpdateUpgrades();
    }

    public virtual void Bind(FarmData data) {
        LateStart();
    }

    protected void InvokeWorkChanged(bool isWork) {
        WorkChanged?.Invoke(isWork);
    }
}
