using System;

[Serializable]
public struct SerializableTimeSpan
{
    public int seconds;
    public int minutes;
    public int hours;

    public SerializableTimeSpan(TimeSpan value)
    {
        hours = value.Hours;
        minutes = value.Minutes;
        seconds = value.Seconds;
    }

    public TimeSpan GetTimeSpan()
    {
        return new TimeSpan(hours, minutes, seconds);
    }
}
