using System;
using UnityEngine;

public class FatigueManager : Singleton<FatigueManager>, IBindable<GeneralData> {
    [SerializeField] private GameTimeManager _timeManager;

    [SerializeField, Min(0)] private float _fatigueMax;
    [SerializeField, Min(0)] private float _fatigueDefault;
    [SerializeField, Min(1)] private float _needHoursToRecovery;

    private float _decorBonus = 1;
    private float _sleepBonus;
    private bool _isTired;
    private FatigueManagerData _data;

    public float FatigueMax => _fatigueMax;
    public float FatigueNow => _data.FatigueNow;
    public bool IsTired => _isTired;

    public event Action<bool> TiredChanged;

    private void LateStart() {
        _sleepBonus = _timeManager.GetSleepBonus(_needHoursToRecovery, _fatigueMax);
    }

    private void Update() {
        if (!_timeManager.IsSleep)
            return;

        _data.ChangeFatigue(-_sleepBonus);
        if (_data.FatigueNow == 0 && _isTired)
            ChangeTiredState(false);
    }

    public void ChangeFatigue(float fatigueValue) {
        _data.ChangeFatigue(fatigueValue / _decorBonus);
        if (_data.FatigueNow >= _fatigueMax)
            ChangeTiredState(true);
    }

    private void ChangeTiredState(bool isTired) {
        _isTired = isTired;
        TiredChanged?.Invoke(_isTired);
        if (!_isTired)
            _timeManager.ChangeSleepState(false);
    }

    public void AddDecorBonus(Decor decor) {
        _decorBonus += decor.FatigueCoef;
    }

    public void Bind(GeneralData data) {
        data.FatigueManager ??= new(_fatigueMax, _fatigueDefault);
        _data = data.FatigueManager;
        LateStart();
    }

    public void MultiplySleepBonus(float coef) {
        _sleepBonus *= coef;
    }
}
