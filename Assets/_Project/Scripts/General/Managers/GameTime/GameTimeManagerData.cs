using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class GameTimeManagerData {
    [SerializeField, JsonProperty] private TimeSpan _globalTime;
    [SerializeField, JsonProperty] private Daytime _daytime;
    [SerializeField, JsonProperty] private bool _isWaitingNextDay;
    [SerializeField, JsonProperty] private int _localDays = 0;

    public TimeSpan GlobalTime => _globalTime;
    public Daytime Daytime => _daytime;
    public bool IsWaitingNextDay => _isWaitingNextDay;
    public int LocalDays => _localDays;

    public GameTimeManagerData(DaytimeStart daytimeStart) {
        _globalTime = new TimeSpan(daytimeStart.Hour, daytimeStart.Minute, 0);
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
        if (_daytime == Daytime.Night)
            _localDays++;

        if (newDaytimeIndex == daytimeCount - 1)
            _isWaitingNextDay = true;
    }
}
