using UnityEngine;

public class ClientTimeMultiplier : MonoBehaviour
{
    [SerializeField] private TimeManager _timeManager;

    [Header("Значения множителя")]
    [SerializeField] private float _morning = 1;
    [SerializeField] private float _day = 1;
    [SerializeField] private float _evening = 1;
    [SerializeField] private float _night = 1;

    private float _daytimeMultiplier = 1;

    public float DaytimeMultiplier => _daytimeMultiplier;

    private void Start()
    {
        _timeManager.DaytimeChanged += ChangeMultiply;
    }

    private void ChangeMultiply(Daytime daytime)
    {
        switch (daytime) {
            case Daytime.Morning: _daytimeMultiplier = _morning; break;
            case Daytime.Day: _daytimeMultiplier = _day; break;
            case Daytime.Evening: _daytimeMultiplier = _evening; break;
            case Daytime.Night: _daytimeMultiplier = _night; break;
        }
    }
}
