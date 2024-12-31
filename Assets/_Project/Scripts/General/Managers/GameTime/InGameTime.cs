using System;
using UnityEngine;

[RequireComponent(typeof(GameTimeManager))]
public class InGameTime : Singleton<InGameTime> {
    private GameTimeManager _timeManager;

    public event Action TimeSpeedUpdated;

    public float NormalizedDeltaTime => _timeManager.NowTimeSpeed / _timeManager.DefaultTimeSpeed * Time.deltaTime;
    public float RawTime => _timeManager.NowTimeSpeed;
    public float DeltaTime => _timeManager.NowTimeSpeed * Time.deltaTime;

    protected override void Awake() {
        base.Awake();
        _timeManager = GetComponent<GameTimeManager>();
        _timeManager.TimeSpeedUpdated += InvokeTimeSpeedUpdate;
    }

    private void InvokeTimeSpeedUpdate() {
        TimeSpeedUpdated?.Invoke();
    }
}
