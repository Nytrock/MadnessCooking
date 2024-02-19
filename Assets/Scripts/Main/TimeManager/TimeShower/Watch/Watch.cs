using UnityEngine;
using TMPro;

public class Watch : TimeRenderer
{
    [SerializeField] private WatchDaytimeRenderer _daytimeRenderer;
    [SerializeField] private TextMeshProUGUI _timeText;

    private void Start()
    {
        _timeManager.DaytimeChanged += _daytimeRenderer.UpdateDaytime;
    }

    protected override void UpdateVisual()
    {
        var timespan = _timeManager.TimeSpan;
        _timeText.text = $"{timespan:hh\\:mm}";
    }
}
