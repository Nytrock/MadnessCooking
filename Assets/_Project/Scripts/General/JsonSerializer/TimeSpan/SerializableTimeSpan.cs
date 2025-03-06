using System;
using UnityEngine;

[Serializable]
public struct SerializableTimeSpan {
    [SerializeField] private int _milliseconds;
    [SerializeField] private int _seconds;
    [SerializeField] private int _minutes;
    [SerializeField] private int _hours;
    [SerializeField] private int _days;

    public SerializableTimeSpan(TimeSpan timeSpan) {
        _days = timeSpan.Days;
        _hours = timeSpan.Hours;
        _minutes = timeSpan.Minutes;
        _seconds = timeSpan.Seconds;
        _milliseconds = timeSpan.Milliseconds;
    }

    public readonly TimeSpan ToTimeSpan() {
        return new TimeSpan(_days, _hours, _minutes, _seconds, _milliseconds);
    }
}
