using System;
using UnityEngine;

public class TimeManager : MonoBehaviour, IBindable<MainData>
{
    public static TimeManager instance;

    [SerializeField] private int _defaultTimeSpeed;
    [SerializeField] private int _sleepTimeSpeed;
    [SerializeField] private DaytimeStart[] _daytimeStarts;

    private int _nowTimeSpeed;
    private MainData _data;

    private TimeSpan _timespan = new(7, 0, 0);
    private Daytime _daytime = Daytime.Morning;

    public TimeSpan TimeSpan => _timespan;
    public int TimeSpeed => _nowTimeSpeed;
    public bool IsSleep => _sleepTimeSpeed == _nowTimeSpeed;

    public event Action<Daytime> DaytimeChanged;

    private void Awake()
    {
        instance = this;
    }

    private void LateStart()
    {
        DaytimeChanged?.Invoke(_daytime);
        _nowTimeSpeed = _defaultTimeSpeed;
    }

    private void Update()
    {
        _timespan = _timespan.Add(new TimeSpan(0, 0, _nowTimeSpeed));
        _data.GlobalTime = new(_timespan);
        CheckDaytime();
    }

    private void CheckDaytime()
    {
        foreach (var daytimeStart in _daytimeStarts) {
            if (daytimeStart.TimeFits(_timespan, _daytime)) {
                ChangeDaytime(daytimeStart.Daytime);
                return;
            }
        }
    }

    private void ChangeDaytime(Daytime _newDaytime)
    {
        _daytime = _newDaytime;
        DaytimeChanged?.Invoke(_daytime);
    }

    public void ChangeSleepState(bool isSleep)
    {
        _nowTimeSpeed = isSleep ? _sleepTimeSpeed : _defaultTimeSpeed;
    }

    public DaytimeStart GetDaytimeStartInfo(Daytime daytime)
    {
        foreach (var daytimeStart in _daytimeStarts)
            if (daytimeStart.Daytime == daytime)
                return daytimeStart;
        return null;
    }

    public float GetSleepBonus(float needHours, float maxFatigue) => maxFatigue / (needHours * 3600 / _sleepTimeSpeed);

    public void Bind(MainData data, bool isFileEmpty)
    {
        _data = data;
        if (isFileEmpty) {
            _data.GlobalTime = new(_timespan);
            LateStart();
            return;
        }

        _timespan = _data.GlobalTime.GetTimeSpan();
        CheckDaytime();
        LateStart();
    }
}
