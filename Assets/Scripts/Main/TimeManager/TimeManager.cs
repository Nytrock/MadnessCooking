using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private int _defaultTimeSpeed = 1;
    [SerializeField] private int _boostedTimeSpeed = 150;

    [SerializeField] private DaytimeDescriber[] _daytimeDescribers;

    private TimeSpan _timespan = new(7, 0, 0);
    private int _timeSpeed;
    private bool _isSkippingNight;

    private Daytime _daytime = Daytime.Morning;

    public TimeSpan TimeSpan => _timespan;

    public event Action<Daytime> DaytimeChanged;

    private void Start()
    {
        _timeSpeed = _defaultTimeSpeed;
        DaytimeChanged?.Invoke(_daytime);
    }

    private void Update()
    {
        _timespan = _timespan.Add(new TimeSpan(0, 0, 1 * _timeSpeed));

        foreach (var describer in _daytimeDescribers) {
            if (describer.TimeFits(_timespan, _daytime)) {
                ChangeDaytime(describer.Daytime);
                return;
            }
        }
    }

    private void ChangeDaytime(Daytime _newDaytime)
    {
        _daytime = _newDaytime;
        DaytimeChanged?.Invoke(_daytime);

        if (_daytime == Daytime.Morning && _isSkippingNight) {
            _isSkippingNight = false;
            _timeSpeed = _defaultTimeSpeed;
        }
    }

    public void SkipNight()
    {
        _timeSpeed = _boostedTimeSpeed;
        _isSkippingNight = true;
    }
}
