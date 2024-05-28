using System;
using UnityEngine;

public class FatigueManager : Singleton<FatigueManager>, IBindable<GeneralData> {
    [SerializeField] private TimeManager _timeManager;

    [SerializeField, Min(0)] private float _fatigueMax;
    [SerializeField, Min(1)] private float _needHoursToRecovery;
    private float _decorBonus = 1;
    private float _sleepBonus;

    private bool _isTired;
    private GeneralData _data;

    public float FatigueMax => _fatigueMax;
    public float FatigueNow => _data.Fatigue;

    public event Action<bool> TiredChanged;

    private void LateStart() {
        _sleepBonus = _timeManager.GetSleepBonus(_needHoursToRecovery, _fatigueMax);
    }

    private void Update() {
        if (_timeManager.IsSleep) {
            _data.Fatigue = Mathf.Max(_data.Fatigue - _sleepBonus, 0);
            if (_data.Fatigue == 0 && _isTired)
                ChangeTiredState(false);
        }
    }

    public void ChangeFatigue(float fatigueValue) {
        _data.Fatigue = Mathf.Clamp(_data.Fatigue + (fatigueValue / _decorBonus), 0, _fatigueMax);
        if (_data.Fatigue >= _fatigueMax)
            ChangeTiredState(true);
    }

    private void ChangeTiredState(bool isTired) {
        _isTired = isTired;
        TiredChanged?.Invoke(_isTired);
        _timeManager.ChangeSleepState(_isTired);
    }

    public void AddDecorBonus(Decor decor) {
        _decorBonus += decor.FatigueCoef;
    }

    public void Bind(GeneralData data, bool isFileEmpty) {
        _data = data;
        LateStart();
    }
}
