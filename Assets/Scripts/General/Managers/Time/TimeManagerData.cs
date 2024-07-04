using System;
using UnityEngine;

[Serializable]
public class TimeManagerData {
    [SerializeField] private SerializableTimeSpan _globalTime;
    [SerializeField] private int _daysCount;

    public TimeSpan GlobalTime => _globalTime.GetTimeSpan();
    public int DaysCount => _daysCount;

    public TimeManagerData(int hours, int minutes = 0, int seconds = 0) {
        _globalTime = new SerializableTimeSpan(hours, minutes, seconds);
        _daysCount = 0;
    }

    public void AddTime(int timeSpeed) {
        _globalTime = _globalTime.Add(new TimeSpan(0, 0, timeSpeed));
    }

    public void AddDay() {
        _daysCount++;
    }
}
