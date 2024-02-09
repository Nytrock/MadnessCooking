using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private TimeSpan _timespan = new(7, 0, 0);
    [SerializeField] private int _timeSpeed = 1;
    private bool _isSkippingNight;

    private Daytime _daytime = Daytime.Morning;
    private Daytime _daytimeProperty
    {
        get { return _daytime; }
        set { 
            if (_daytime != value) {
                _daytime = value;
                DaytimeChanged?.Invoke(_daytime);
            }
        }
    }

    public TimeSpan TimeSpan => _timespan;

    public event Action<Daytime> DaytimeChanged;

    private void Start()
    {
        DaytimeChanged?.Invoke(_daytime);
    }

    private void Update()
    {
        _timespan = _timespan.Add(new TimeSpan(0, 0, 1 * _timeSpeed));
        switch (_timespan.Hours) {
            case 4: 
                _daytimeProperty = Daytime.Morning; 
                if (_isSkippingNight) {
                    _isSkippingNight = false;
                    _timeSpeed = 1;
                }
                break;
            case 10:
                _daytimeProperty = Daytime.Day; 
                break;
            case 17: 
                _daytimeProperty = Daytime.Evening; 
                break;
            case 22: 
                _daytimeProperty = Daytime.Night; 
                break;
        }
    }

    public void SkipNight()
    {
        _timeSpeed = 150;
        _isSkippingNight = true;
    }
}
