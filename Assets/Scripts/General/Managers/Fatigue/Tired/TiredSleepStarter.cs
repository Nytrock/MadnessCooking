using UnityEngine;

public class TiredSleepStarter : MonoBehaviour {
    [SerializeField] private TimeManager _timeManager;

    public void StartTiredSleep() {
        _timeManager.ChangeSleepState(true);
    }
}
