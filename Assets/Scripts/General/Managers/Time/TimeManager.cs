using System;
using UnityEngine;

public class TimeManager : MonoBehaviour, IBindable<GeneralData> {
    [SerializeField, Min(0)] private int _defaultTimeSpeed;
    [SerializeField, Min(0)] private int _sleepTimeSpeed;
    [SerializeField] private DaytimeStart[] _daytimeStarts;

    private int _nowTimeSpeed;
    private Daytime _daytime = Daytime.Morning;
    private TimeManagerData _data;

    public TimeSpan GlobalTime => _data.GlobalTime;
    public int DaysCount => _data.DaysCount;
    public bool IsSleep => _sleepTimeSpeed == _nowTimeSpeed;
    public int NowTimeSpeed => _nowTimeSpeed;

    public event Action<Daytime> DaytimeChanged;

    private void LateStart() {
        DaytimeChanged?.Invoke(_daytime);
        _nowTimeSpeed = _defaultTimeSpeed;
    }

    private void Update() {
        _data.AddTime(_nowTimeSpeed);
        CheckDaytime();
    }

    private void CheckDaytime() {
        foreach (var daytimeStart in _daytimeStarts) {
            if (daytimeStart.TimeFits(_data.GlobalTime, _daytime)) {
                ChangeDaytime(daytimeStart.Daytime);
                return;
            }
        }
    }

    private void ChangeDaytime(Daytime newDaytime) {
        _daytime = newDaytime;
        if (_daytime == Daytime.Night)
            _data.AddDay();

        DaytimeChanged?.Invoke(_daytime);
    }

    public void ChangeSleepState(bool isSleep) {
        _nowTimeSpeed = isSleep ? _sleepTimeSpeed : _defaultTimeSpeed;
    }

    public DaytimeStart GetDaytimeStartInfo(Daytime daytime) {
        foreach (var daytimeStart in _daytimeStarts)
            if (daytimeStart.Daytime == daytime)
                return daytimeStart;
        throw new NullReferenceException($"No info about {daytime}");
    }

    public float GetSleepBonus(float needHours, float maxFatigue) {
        return maxFatigue / (needHours * 3600 / _sleepTimeSpeed);
    }

    public void Bind(GeneralData data, bool isFileEmpty) {
        if (isFileEmpty) {
            DaytimeStart defaultDaytimeStart = GetDaytimeStartInfo(_daytime);
            data.TimeManager = new(defaultDaytimeStart.Hour, defaultDaytimeStart.Minute);
        }
        _data = data.TimeManager;

        CheckDaytime();
        LateStart();
    }
}
