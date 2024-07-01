using System;
using UnityEngine;

[Serializable]
public class TimeManagerData {
    [SerializeField] private SerializableTimeSpan _globalTime;

    public SerializableTimeSpan GlobalTime => _globalTime;

    public TimeManagerData(TimeSpan defaultTimeSpan) {
        _globalTime = new SerializableTimeSpan(defaultTimeSpan);
    }

    public void AddTime(int timeSpeed) {
        _globalTime = _globalTime.Add(new TimeSpan(0, 0, timeSpeed));
    }
}
