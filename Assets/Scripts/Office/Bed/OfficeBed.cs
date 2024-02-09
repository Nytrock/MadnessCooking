using UnityEngine;

public class OfficeBed : MonoBehaviour
{
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private OfficeBedUI _officeBedUI;

    private void Start()
    {
        _timeManager.DaytimeChanged += _officeBedUI.CheckButton;
    }

    public void StartSleep()
    {
        _officeBedUI.StartSleep();
        _timeManager.SkipNight();
    }
}
