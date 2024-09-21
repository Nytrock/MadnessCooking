using UnityEngine;

public class MoonRotater : AstronomicalObjectsRotater {
    [SerializeField] private SpriteRenderer _moon;
    [SerializeField] private Sprite[] _moonPhases;

    protected override Daytime _startDaytime => Daytime.Night;
    protected override Daytime _endDaytime => Daytime.Morning;

    private void Awake() {
        _timeManager.DaytimeChanged += CheckNigthStart;
    }

    private void CheckNigthStart(Daytime daytime) {
        if (daytime == _startDaytime)
            _moon.sprite = _moonPhases[_timeManager.DaysCount % _moonPhases.Length];
    }
}
