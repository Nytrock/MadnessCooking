using UnityEngine;
using TMPro;
using System;

public class Watch : TimeShower
{
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _daytimeText;

    private void Start()
    {
        _timeManager.DaytimeChanged += UpdateDaytime;
    }

    protected override void UpdateVisual()
    {
        var timespan = _timeManager.TimeSpan;
        _timeText.text = $"{timespan:hh\\:mm}";
    }

    private void UpdateDaytime(Daytime daytime)
    {
        _daytimeText.text = daytime.ToString();
    }
}
