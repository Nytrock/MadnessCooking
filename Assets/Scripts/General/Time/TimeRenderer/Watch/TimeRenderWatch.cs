using System;
using TMPro;
using UnityEngine;

public class TimeRenderWatch : TimeRenderer
{
    [SerializeField] private TextMeshProUGUI _timeText;

    protected override void UpdateVisual()
    {
        TimeSpan timespan = _timeManager.TimeSpan;
        _timeText.text = $"{timespan:hh\\:mm}";
    }
}
