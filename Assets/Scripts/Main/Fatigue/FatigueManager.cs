using System;
using UnityEngine;

public class FatigueManager : MonoBehaviour, IBindable<MainData>
{
    public static FatigueManager instance;

    [SerializeField] private TimeManager _timeManager;

    [SerializeField] private float _fatigueMax;
    [SerializeField] private float _needHoursToRecovery;
    private float _fatigueNow = 0;
    private float _decorBonus = 1;
    private float _sleepBonus;

    private bool _isTired;
    private MainData _data;

    public float FatigueMax => _fatigueMax;
    public float FatigueNow => _fatigueNow;

    public event Action<bool> TiredChanged;

    private void Awake()
    {
        instance = this;
    }

    private void LateStart()
    {
        _sleepBonus = _timeManager.GetSleepBonus(_needHoursToRecovery, _fatigueMax);
    }

    private void Update()
    {
        if (_timeManager.IsSleep) {
            _fatigueNow = Mathf.Max(_fatigueNow - _sleepBonus, 0);
            if (_fatigueNow == 0 && _isTired)
                ChangeTiredState(false);
        }
    }

    public void ChangeFatigue(float fatigueValue)
    {
        _fatigueNow = Mathf.Clamp(_fatigueNow + fatigueValue / _decorBonus, 0, _fatigueMax);
        _data.Fatigue = _fatigueNow;
        if (_fatigueNow >= _fatigueMax)
            ChangeTiredState(true);
    }

    private void ChangeTiredState(bool isTired)
    {
        _isTired = isTired;
        TiredChanged?.Invoke(_isTired);
        _timeManager.ChangeSleepState(_isTired);
    }

    public void AddDecorBonus(Decor decor)
    {
        _decorBonus += decor.FatigueCoef;
    }

    public void Bind(MainData data, bool isFileEmpty)
    {
        _data = data;
        if (isFileEmpty) {
            _data.Fatigue = _fatigueNow;
            LateStart();
            return;
        }

        _fatigueNow = _data.Fatigue;
        LateStart();
    }
}
