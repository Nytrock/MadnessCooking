using UnityEngine;

public class Clock : TimeShower
{
    private const float
        hoursToDegrees = 360f / 12f,
        minutesToDegrees = 360f / 60f;

    public Transform hoursPoint, minutesPoint;

    protected override void UpdateVisual()
    {
        var timespan = _timeManager.TimeSpan;
        hoursPoint.localRotation = Quaternion.Euler(
                0f, 0f, (float)timespan.TotalHours * -hoursToDegrees);
        minutesPoint.localRotation = Quaternion.Euler(
                0f, 0f, (float)timespan.TotalMinutes * -minutesToDegrees);
    }
}
