using UnityEngine;

public class TiredSleepStarter : MonoBehaviour {
    [SerializeField] private SleepBed _bed;

    public void StartTiredSleep() {
        _bed.ChangeSleepState(true);
    }
}
