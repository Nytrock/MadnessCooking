using MadnessCooking.General;
using UnityEngine;

namespace MadnessCooking.Office {
    public class SleepBedUI : ActivableUI {
        [SerializeField] private SleepBed _sleepBed;
        [SerializeField] private GameObject _panel;
        [SerializeField] private GameObject _blockPanel;
        [SerializeField] private LocalizedText _sleepButtonText;
        [SerializeField] private string _sleepingNote;
        [SerializeField] private string _notSleepingNote;

        private void Awake() {
            _sleepBed.SleepChanged += UpdateSleepState;
        }

        public override void ChangeState(bool newState) {
            _panel.SetActive(newState);
            base.ChangeState(newState);
        }

        public void UpdateSleepState(bool isSleep) {
            _blockPanel.SetActive(isSleep);
            if (isSleep)
                _sleepButtonText.SetText(_sleepingNote);
            else
                _sleepButtonText.SetText(_notSleepingNote);
        }
    }
}
