using UnityEngine;

public class SleepBedActivator : UIActivator {
    [SerializeField] private SleepBed _bed;

    protected override void Press() {
        if (_bed.IsSleep)
            return;

        base.Press();
    }
}
