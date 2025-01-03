using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class GameTimeManagerData {
    [SerializeField, JsonProperty] private JsonTimeSpan _globalTime;
    [SerializeField, JsonProperty] private Daytime _daytime;
    [SerializeField, JsonProperty] private bool _isWaitingNextDay;

    public TimeSpan GlobalTime => _globalTime.GetTimeSpan();
    public Daytime Daytime => _daytime;
    public bool IsWaitingNextDay => _isWaitingNextDay;

    public GameTimeManagerData(DaytimeStart daytimeStart) {
        _globalTime = new JsonTimeSpan(daytimeStart.Hour, daytimeStart.Minute);
        _daytime = daytimeStart.Daytime;
    }

    public void AddTime(float timeSpeed) {
        timeSpeed *= FpsManager.NORMALIZED_DELTA_TIME;

        int oldDayCount = _globalTime.Days;
        int seconds = Mathf.FloorToInt(timeSpeed);
        int milliseconds = Mathf.FloorToInt(timeSpeed % 1 * 1000);

        _globalTime = _globalTime.Add(new TimeSpan(0, 0, 0, seconds, milliseconds));
        if (_isWaitingNextDay && oldDayCount < _globalTime.Days)
            _isWaitingNextDay = false;
    }

    public void ChangeDaytime(int newDaytimeIndex, int daytimeCount) {
        _daytime = (Daytime)newDaytimeIndex;
        if (newDaytimeIndex == daytimeCount - 1)
            _isWaitingNextDay = true;
    }
}
