using System;
using UnityEngine;

public class OfficeBed : MonoBehaviour, IBindable<OfficeData> {
    [SerializeField] private TimeManager _timeManager;
    private OfficeBedData _data;

    public event Action<bool> SleepChanged;

    public void Bind(OfficeData data) {
        data.OfficeBed ??= new();
        _data = data.OfficeBed;
        UpdateSleepState();
    }

    public void ChangeSleepState() {
        _data.ChangeSleepState();
        UpdateSleepState();
    }

    private void UpdateSleepState() {
        _timeManager.ChangeSleepState(_data.IsSleep);
        SleepChanged?.Invoke(_data.IsSleep);
    }
}
