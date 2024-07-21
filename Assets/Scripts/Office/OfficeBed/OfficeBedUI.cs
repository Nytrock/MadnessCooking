using UnityEngine;

public class OfficeBedUI : MonoBehaviour, IActivable {
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _blockPanel;
    [SerializeField] private LocalizedText _sleepButtonText;
    [SerializeField] private string _sleepingNote;
    [SerializeField] private string _notSleepingNote;

    public void LateStart(bool isSleep) {
        _panel.SetActive(isSleep);
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
