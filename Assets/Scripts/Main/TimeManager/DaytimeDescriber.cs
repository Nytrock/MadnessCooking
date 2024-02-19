using System;
using UnityEngine;

[Serializable]
public class DaytimeDescriber
{
    [SerializeField] private Daytime _daytime;
    [SerializeField, Range(0, 23)] private int _startHour;
    [SerializeField, Range(0, 59)] private int _startMinute;

    public Daytime Daytime => _daytime;

    public bool TimeFits(TimeSpan nowTime, Daytime nowDaytime)
    {
        if (nowDaytime == _daytime)
            return false;

        return nowTime.Hours == _startHour && nowTime.Minutes == _startMinute;
    }
}
