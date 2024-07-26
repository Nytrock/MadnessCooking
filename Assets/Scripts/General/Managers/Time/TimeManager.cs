using System;
using UnityEngine;

public class TimeManager : MonoBehaviour, IBindable<GeneralData> {
    [SerializeField, Min(0)] private int _defaultTimeSpeed;
    [SerializeField, Min(0)] private int _sleepTimeSpeed;
    [SerializeField] private DaytimeStart[] _daytimeStarts;
    [SerializeField] private Daytime _defaultDaytime;

    private int _daytimeCount;
    private int _nowTimeSpeed;
    [SerializeField] private TimeManagerData _data;

    public TimeSpan GlobalTime => _data.GlobalTime;
    public int DaysCount => _data.GlobalTime.Days;
    public bool IsSleep => _sleepTimeSpeed == _nowTimeSpeed;
    public int NowTimeSpeed => _nowTimeSpeed;

    public event Action<Daytime> DaytimeChanged;

    private void Awake() {
        _daytimeCount = Enum.GetNames(typeof(Daytime)).Length;
    }

    private void LateStart() {
        DaytimeChanged?.Invoke(_data.Daytime);
        _nowTimeSpeed = _defaultTimeSpeed;
    }

    private void Update() {
        _data.AddTime(_nowTimeSpeed);
        CheckDaytime();
    }

    private void CheckDaytime() {
        if (_data.IsWaitingNextDay)
            return;

        int nextDaytimeIndex = ((int)_data.Daytime + 1) % _daytimeCount;
        if (_daytimeStarts[nextDaytimeIndex].TimeFits(_data.GlobalTime))
            ChangeDaytime(nextDaytimeIndex);
    }

    private void ChangeDaytime(int newDaytimeIndex) {
        _data.ChangeDaytime(newDaytimeIndex, _daytimeCount);
        DaytimeChanged?.Invoke(_data.Daytime);
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
            DaytimeStart defaultDaytimeStart = GetDaytimeStartInfo(_defaultDaytime);
            data.TimeManager = new(defaultDaytimeStart);
        }
        _data = data.TimeManager;
        LateStart();
    }
}
