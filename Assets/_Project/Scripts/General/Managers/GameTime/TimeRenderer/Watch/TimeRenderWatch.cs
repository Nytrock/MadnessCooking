using System;
using TMPro;
using UnityEngine;

namespace MadnessCooking.General {
    public class TimeRenderWatch : TimeRenderer {
        [SerializeField] private TextMeshProUGUI _timeText;

        protected override void UpdateVisual() {
            TimeSpan timespan = _timeManager.GlobalTime;
            _timeText.text = $"{timespan:hh\\:mm}";
        }
    }
}
