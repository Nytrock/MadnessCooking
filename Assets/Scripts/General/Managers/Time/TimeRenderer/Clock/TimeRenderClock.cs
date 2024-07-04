using System;
using UnityEngine;

public class TimeRenderClock : TimeRenderer {
    private const float
        HOURS_TO_DEGREES = 360f / 12f,
        MINUTES_TO_DEGREES = 360f / 60f;

    [SerializeField] private Transform _hoursPoint;
    [SerializeField] private Transform _minutesPoint;

    protected override void UpdateVisual() {
        TimeSpan timespan = _timeManager.GlobalTime;
        _hoursPoint.localRotation = Quaternion.Euler(
                0f, 0f, (float)timespan.TotalHours * -HOURS_TO_DEGREES);
        _minutesPoint.localRotation = Quaternion.Euler(
                0f, 0f, (float)timespan.TotalMinutes * -MINUTES_TO_DEGREES);
    }
}
