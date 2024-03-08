using UnityEngine;

public class OfficeBed : MonoBehaviour
{
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private OfficeBedUI _officeBedUI;
    private bool _isSleeping;

    public void ChangeSleepState()
    {
        _isSleeping = !_isSleeping;
        _officeBedUI.ChangeButtonText(_isSleeping);
        _timeManager.ChangeSleepState(_isSleeping);
    }
}
