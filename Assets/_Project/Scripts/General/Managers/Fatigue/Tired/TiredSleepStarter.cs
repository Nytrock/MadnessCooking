using UnityEngine;
using MadnessCooking.Office;

namespace MadnessCooking.General {
    public class TiredSleepStarter : MonoBehaviour {
        [SerializeField] private SleepBed _bed;

        public void StartTiredSleep() {
            _bed.ChangeSleepState(true);
        }
    }
}
