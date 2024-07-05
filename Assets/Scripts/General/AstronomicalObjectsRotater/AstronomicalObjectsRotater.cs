using System;
using UnityEngine;

public abstract class AstronomicalObjectsRotater : MonoBehaviour {
    [SerializeField] protected TimeManager _timeManager;
    [SerializeField] private float _startDegree;
    [SerializeField] private float _endDegree;

    protected abstract Daytime _startDaytime { get; }
    protected abstract Daytime _endDaytime { get; }

    protected int _startSeconds;
    protected int _endSeconds;
    private bool _isGoingThroughtZero;

    private const int SECONDS_IN_MINUTE = 60;
    private const int SECONDS_IN_HOUR = 3600;

    private void Start() {
        _startSeconds = CalculateDaytimeSeconds(_startDaytime);
        _endSeconds = CalculateDaytimeSeconds(_endDaytime);
        _isGoingThroughtZero = _startSeconds > _endSeconds;
    }

    private void Update() {
        UpdateAngle();
    }

    private int CalculateDaytimeSeconds(Daytime daytime) {
        DaytimeStart daytimeStart = _timeManager.GetDaytimeStartInfo(daytime);
        return (daytimeStart.Minute * SECONDS_IN_MINUTE) + (daytimeStart.Hour * SECONDS_IN_HOUR);
    }

    private void UpdateAngle() {
        TimeSpan nowTime = _timeManager.GlobalTime;
        int nowSeconds = nowTime.Seconds + (nowTime.Minutes * SECONDS_IN_MINUTE)
            + (nowTime.Hours * SECONDS_IN_HOUR);

        float startSeconds = _startSeconds, endSeconds = _endSeconds;
        if (_isGoingThroughtZero) {
            if (nowSeconds < _startSeconds)
                startSeconds -= 24 * SECONDS_IN_HOUR;
            else if (nowSeconds > _endSeconds)
                endSeconds += 24 * SECONDS_IN_HOUR;
        }

        float degreeCoef = Mathf.InverseLerp(startSeconds, endSeconds, nowSeconds);
        float degree = Mathf.LerpAngle(_startDegree, _endDegree, degreeCoef);
        transform.localRotation = Quaternion.Euler(0f, 0f, degree);
    }
}
