using UnityEngine;

public class OfficeBed : MonoBehaviour, IBindable<OfficeData>
{
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private OfficeBedUI _officeBedUI;
    private OfficeData _data;

    public void Bind(OfficeData data, bool isFileEmpty)
    {
        _data = data;

        if (_officeBedUI != null)
            _officeBedUI.LateStart(_data.IsSleeping);
        UpdateSleepState();
    }

    public void ChangeSleepState()
    {
        _data.IsSleeping = !_data.IsSleeping;
        UpdateSleepState();
    }

    private void UpdateSleepState()
    {
        _timeManager.ChangeSleepState(_data.IsSleeping);

        if (_officeBedUI != null)
            _officeBedUI.UpdateSleepState(_data.IsSleeping);
    }
}
