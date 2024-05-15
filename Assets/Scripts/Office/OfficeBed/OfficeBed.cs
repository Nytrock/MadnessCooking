using UnityEngine;

public class OfficeBed : MonoBehaviour, IBindable<OfficeData>
{
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private OfficeBedUI _officeBedUI;
    private OfficeData _data;

    public void Bind(OfficeData data, bool isFileEmpty)
    {
        _data = data;
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
        _officeBedUI.UpdateSleepState(_data.IsSleeping);
        _timeManager.ChangeSleepState(_data.IsSleeping);
    }
}
