using UnityEngine;

public class TiredSleepStarter : MonoBehaviour {
    [SerializeField] private GameTimeManager _timeManager;
    [SerializeField] private SleepBed _bed;

    public void StartTiredSleep() {
        _timeManager.ChangeTimeSpeed(_bed.SleepTimeSpeed);
    }
}
