using System;
using UnityEngine;

public class GameTimeManager : MonoBehaviour, IBindable<GeneralData> {
    [SerializeField, Min(0)] private int _defaultTimeSpeed;
    [SerializeField, Min(0)] private int _sleepTimeSpeed;
    [SerializeField] private PauseManager _pauseManager;
    [SerializeField] private DaytimeStart[] _daytimeStarts;
    [SerializeField] private Daytime _defaultDaytime;

    private int _daytimeCount;
    [SerializeField] private int _nowTimeSpeed;
    private int _previousTimeSpeed;
    private GameTimeManagerData _data;

    public TimeSpan GlobalTime => _data.GlobalTime;
    public int DaysCount => _data.GlobalTime.Days;
    public bool IsSleep => _sleepTimeSpeed == _nowTimeSpeed;

    public int NormalizedNowTimeSpeed => _nowTimeSpeed / _defaultTimeSpeed;
    public int NowTimeSpeed => _nowTimeSpeed;

    public event Action<Daytime> DaytimeChanged;
    public event Action TimeSpeedUpdated;

    private void Awake() {
        _daytimeCount = Enum.GetNames(typeof(Daytime)).Length;
        _pauseManager.PauseChanged += ChangePauseState;
    }

    private void LateStart() {
        DaytimeChanged?.Invoke(_data.Daytime);
        _nowTimeSpeed = _defaultTimeSpeed;
        _previousTimeSpeed = _nowTimeSpeed;
        TimeSpeedUpdated?.Invoke();
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
        TimeSpeedUpdated?.Invoke();
    }

    private void ChangePauseState(bool isPause) {
        if (isPause)
            _previousTimeSpeed = _nowTimeSpeed;
        _nowTimeSpeed = isPause ? 0 : _previousTimeSpeed;
        TimeSpeedUpdated?.Invoke();
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

    public void Bind(GeneralData data) {
        if (data.GameTimeManager == null) {
            DaytimeStart defaultDaytimeStart = GetDaytimeStartInfo(_defaultDaytime);
            data.GameTimeManager = new(defaultDaytimeStart);
        }

        _data = data.GameTimeManager;
        LateStart();
    }
}
