using UnityEngine;

public class SleepBedUI : MonoBehaviour, IActivable {
    [SerializeField] private SleepBed _sleepBed;
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _blockPanel;
    [SerializeField] private LocalizedText _sleepButtonText;
    [SerializeField] private string _sleepingNote;
    [SerializeField] private string _notSleepingNote;

    private void Awake() {
        _sleepBed.SleepChanged += UpdateSleepState;
    }

    public void ChangeState(bool newState) {
        _panel.SetActive(newState);
    }

    public void ChangeState() {
        _panel.SetActive(!_panel.activeSelf);
    }

    public void UpdateSleepState(bool isSleep) {
        _blockPanel.SetActive(isSleep);
        if (isSleep)
            _sleepButtonText.SetText(_sleepingNote);
        else
            _sleepButtonText.SetText(_notSleepingNote);
    }
}
