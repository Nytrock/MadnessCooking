using UnityEngine;
using TMPro;

public class Watch : TimeRenderer
{
    [SerializeField] private TextMeshProUGUI _timeText;

    protected override void UpdateVisual()
    {
        var timespan = _timeManager.TimeSpan;
        _timeText.text = $"{timespan:hh\\:mm}";
    }
}
