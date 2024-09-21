using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TimeRenderWatch))]
public class WatchDaytimeRenderer : MonoBehaviour {
    [SerializeField] private GameTimeManager _timeManager;
    [SerializeField] private Image _daytimeIcon;
    [SerializeField] private LocalizedText _daytimeText;
    [SerializeField] private DaytimeRenderInfo[] _daytimeInfos;

    private void Awake() {
        _timeManager.DaytimeChanged += UpdateDaytime;
    }

    public void UpdateDaytime(Daytime daytime) {
        DaytimeRenderInfo newDaytime = FindDaytime(daytime);

        _daytimeIcon.sprite = newDaytime.Icon;
        _daytimeIcon.color = newDaytime.Color;

        _daytimeText.SetColor(newDaytime.Color);
        _daytimeText.SetText(daytime.GetText());
    }

    private DaytimeRenderInfo FindDaytime(Daytime daytime) {
        foreach (var info in _daytimeInfos)
            if (info.Daytime == daytime)
                return info;

        return null;
    }
}
