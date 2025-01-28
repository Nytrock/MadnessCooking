using System;
using UnityEngine;

public class SleepBedUI : MonoBehaviour, IActivable {
    [SerializeField] private SleepBed _sleepBed;
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _blockPanel;
    [SerializeField] private LocalizedText _sleepButtonText;
    [SerializeField] private string _sleepingNote;
    [SerializeField] private string _notSleepingNote;

    public event Action<bool> StateChanged;

    private void Awake() {
        _sleepBed.SleepChanged += UpdateSleepState;
    }

    public void ChangeState(bool newState) {
        _panel.SetActive(newState);
        StateChanged?.Invoke(newState);
    }

    public void UpdateSleepState(bool isSleep) {
        _blockPanel.SetActive(isSleep);
        if (isSleep)
            _sleepButtonText.SetText(_sleepingNote);
        else
            _sleepButtonText.SetText(_notSleepingNote);
    }
}
