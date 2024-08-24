using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public struct JsonTimeSpan {
    [SerializeField, JsonProperty] private int _seconds;
    [SerializeField, JsonProperty] private int _minutes;
    [SerializeField, JsonProperty] private int _hours;
    [SerializeField, JsonProperty] private int _days;

    public int Days => _days;

    public JsonTimeSpan(TimeSpan value) {
        _hours = value.Hours;
        _minutes = value.Minutes;
        _seconds = value.Seconds;
        _days = value.Days;
    }

    public JsonTimeSpan(int hours, int minutes) {
        _hours = hours;
        _minutes = minutes;
        _seconds = 0;
        _days = 0;
    }

    public TimeSpan GetTimeSpan() {
        return new TimeSpan(_days, _hours, _minutes, _seconds);
    }

    public JsonTimeSpan Add(TimeSpan timeSpan) {
        return new JsonTimeSpan(GetTimeSpan().Add(timeSpan));
    }
}
