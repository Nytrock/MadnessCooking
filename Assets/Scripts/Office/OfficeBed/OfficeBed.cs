using UnityEngine;

public class OfficeBed : MonoBehaviour, IBindable<OfficeData> {
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private OfficeBedUI _officeBedUI;
    private OfficeBedData _data;

    public void Bind(OfficeData data) {
        data.OfficeBed ??= new();
        _data = data.OfficeBed;

        if (_officeBedUI != null)
            _officeBedUI.LateStart(_data.IsSleeping);
        UpdateSleepState();
    }

    public void ChangeSleepState() {
        _data.ChangeSleepState();
        UpdateSleepState();
    }

    private void UpdateSleepState() {
        _timeManager.ChangeSleepState(_data.IsSleeping);

        if (_officeBedUI != null)
            _officeBedUI.UpdateSleepState(_data.IsSleeping);
    }
}
