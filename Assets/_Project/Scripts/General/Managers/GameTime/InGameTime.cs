using System;
using UnityEngine;

[RequireComponent(typeof(GameTimeManager))]
public class InGameTime : Singleton<InGameTime> {
    private GameTimeManager _timeManager;

    public event Action TimeSpeedUpdated;

    public float NormalizedTime => _timeManager.NowTimeSpeed / _timeManager.DefaultTimeSpeed;
    public float NormalizedDeltaTime => NormalizedTime * Time.deltaTime;
    public float RawTime => _timeManager.NowTimeSpeed;
    public float RawDeltaTime => RawTime * Time.deltaTime;

    protected override void Awake() {
        base.Awake();
        _timeManager = GetComponent<GameTimeManager>();
        _timeManager.TimeSpeedUpdated += InvokeTimeSpeedUpdate;
    }

    private void InvokeTimeSpeedUpdate() {
        TimeSpeedUpdated?.Invoke();
    }
}
