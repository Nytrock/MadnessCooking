using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WatchDaytimeRenderer : MonoBehaviour
{
    [SerializeField] private Image _daytimeIcon;
    [SerializeField] private TextMeshProUGUI _daytimeText;
    [SerializeField] private DaytimeRenderInfo[] _daytimeInfos;

    public void UpdateDaytime(Daytime daytime)
    {
        var newDaytime = FindDaytime(daytime);
        _daytimeIcon.sprite = newDaytime.Icon;
        _daytimeText.color = newDaytime.Color;
        _daytimeText.text = daytime.ToString();
    }

    private DaytimeRenderInfo FindDaytime(Daytime daytime)
    {
        foreach (var info in _daytimeInfos)
            if (info.Daytime == daytime)
                return info;

        return null;
    }
}
