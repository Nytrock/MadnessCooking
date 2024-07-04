using System;
using UnityEngine;

[Serializable]
public struct SerializableTimeSpan {
    [SerializeField] private int _seconds;
    [SerializeField] private int _minutes;
    [SerializeField] private int _hours;

    public SerializableTimeSpan(TimeSpan value) {
        _hours = value.Hours;
        _minutes = value.Minutes;
        _seconds = value.Seconds;
    }

    public SerializableTimeSpan(int hours, int minutes = 0, int seconds = 0) {
        _hours = hours;
        _minutes = minutes;
        _seconds = seconds;
    }

    public TimeSpan GetTimeSpan() {
        return new TimeSpan(_hours, _minutes, _seconds);
    }

    public SerializableTimeSpan Add(TimeSpan timeSpan) {
        return new SerializableTimeSpan(GetTimeSpan().Add(timeSpan));
    }
}
