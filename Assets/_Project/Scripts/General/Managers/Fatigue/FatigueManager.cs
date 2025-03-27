using System;
using UnityEngine;

public class FatigueManager : Singleton<FatigueManager>, IBindable<GeneralData>, IUpgradeable<OfficeUpgradeData> {
    [SerializeField] private GameTimeManager _timeManager;
    [SerializeField] private SleepBed _bed;

    [SerializeField, Min(0)] private float _fatigueMax;
    [SerializeField, Min(0)] private float _fatigueDefault;
    [SerializeField, Min(1)] private float _needHoursToRecovery;
    [SerializeField] private DecorManager _decorManager;

    private float _decorBonus = 1;
    private float _sleepBonus;
    private bool _isTired;

    [SerializeField] private FatigueManagerData _data;
    private OfficeUpgradeData _upgradeData;

    public float FatigueMax => _fatigueMax;
    public float FatigueNow => _data.FatigueNow;
    public bool IsTired => _isTired;

    public event Action<bool> TiredChanged;

    protected override void Awake() {
        base.Awake();
        _decorManager.ItemAdded += AddDecorBonus;
    }

    public void LateStart() {
        _sleepBonus = _bed.GetSleepBonus(_needHoursToRecovery, _fatigueMax);
    }

    private void Update() {
        if (!_bed.IsSleep)
            return;

        UpdateSleepState();
    }

    private void UpdateSleepState() {
        RemoveFatigue(_sleepBonus * _upgradeData.SleepCoef * FpsManager.NORMALIZED_DELTA_TIME);
        if (_data.FatigueNow == 0 && _isTired)
            ChangeTiredState(false);
    }

    public void RemoveFatigue(float cheerfullValue) {
        if (cheerfullValue < 0)
            return;

        _data.ChangeFatigue(-cheerfullValue);
    }

    public void AddFatigue(float fatigueValue) {
        if (_isTired || fatigueValue <= 0)
            return;

        _data.ChangeFatigue(fatigueValue / _decorBonus);
        if (_data.FatigueNow >= _fatigueMax)
            ChangeTiredState(true);
    }

    private void ChangeTiredState(bool isTired) {
        _isTired = isTired;
        TiredChanged?.Invoke(_isTired);
        if (!_isTired)
            _bed.ChangeSleepState(false);
    }

    public void AddDecorBonus(Decor decor) {
        _decorBonus += decor.FatigueCoef;
    }

    public void Bind(GeneralData data) {
        data.FatigueManager ??= new(_fatigueMax, _fatigueDefault);
        _data = data.FatigueManager;
        _data.SetFatigueMax(_fatigueMax);
    }

    public void BindUpgrade(OfficeUpgradeData upgradeData) {
        _upgradeData = upgradeData;
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) { }
}
