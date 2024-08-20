using UnityEngine;

public class TiredSleepStarter : MonoBehaviour {
    [SerializeField] private GameTimeManager _timeManager;

    public void StartTiredSleep() {
        _timeManager.ChangeSleepState(true);
    }
}
