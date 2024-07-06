using System;
using UnityEngine;

[Serializable]
public struct SerializableTimeSpan {
    [SerializeField] private int _seconds;
    [SerializeField] private int _minutes;
    [SerializeField] private int _hours;
    [SerializeField] private int _days;

    public int Days => _days;

    public SerializableTimeSpan(TimeSpan value) {
        _hours = value.Hours;
        _minutes = value.Minutes;
        _seconds = value.Seconds;
        _days = value.Days;
    }

    public SerializableTimeSpan(int hours, int minutes) {
        _hours = hours;
        _minutes = minutes;
        _seconds = 0;
        _days = 0;
    }

    public TimeSpan GetTimeSpan() {
        return new TimeSpan(_days, _hours, _minutes, _seconds);
    }

    public SerializableTimeSpan Add(TimeSpan timeSpan) {
        return new SerializableTimeSpan(GetTimeSpan().Add(timeSpan));
    }
}
