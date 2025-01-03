using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public struct JsonTimeSpan {
    [SerializeField, JsonProperty] private int _milliseconds;
    [SerializeField, JsonProperty] private int _seconds;
    [SerializeField, JsonProperty] private int _minutes;
    [SerializeField, JsonProperty] private int _hours;
    [SerializeField, JsonProperty] private int _days;

    public readonly int Days => _days;

    public JsonTimeSpan(TimeSpan value) {
        _days = value.Days;
        _hours = value.Hours;
        _minutes = value.Minutes;
        _seconds = value.Seconds;
        _milliseconds = value.Milliseconds;
    }

    public JsonTimeSpan(int hours, int minutes) {
        _hours = hours;
        _minutes = minutes;
        _seconds = 0;
        _milliseconds = 0;
        _days = 0;
    }

    public readonly TimeSpan GetTimeSpan() {
        return new TimeSpan(_days, _hours, _minutes, _seconds, _milliseconds);
    }

    public readonly JsonTimeSpan Add(TimeSpan timeSpan) {
        return new JsonTimeSpan(GetTimeSpan().Add(timeSpan));
    }
}
