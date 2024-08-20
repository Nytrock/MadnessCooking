using System;
using UnityEngine;

[Serializable]
public class GameTimeManagerData {
    [SerializeField] private SerializableTimeSpan _globalTime;
    [SerializeField] private Daytime _daytime;
    [SerializeField] private bool _isWaitingNextDay;

    public TimeSpan GlobalTime => _globalTime.GetTimeSpan();
    public Daytime Daytime => _daytime;
    public bool IsWaitingNextDay => _isWaitingNextDay;

    public GameTimeManagerData(DaytimeStart daytimeStart) {
        _globalTime = new SerializableTimeSpan(daytimeStart.Hour, daytimeStart.Minute);
        _daytime = daytimeStart.Daytime;
    }

    public void AddTime(int timeSpeed) {
        int oldDayCount = _globalTime.Days;
        _globalTime = _globalTime.Add(new TimeSpan(0, 0, timeSpeed));
        if (_isWaitingNextDay && oldDayCount < _globalTime.Days)
            _isWaitingNextDay = false;
    }

    public void ChangeDaytime(int newDaytimeIndex, int daytimeCount) {
        _daytime = (Daytime)newDaytimeIndex;
        if (newDaytimeIndex == daytimeCount - 1)
            _isWaitingNextDay = true;
    }
}
