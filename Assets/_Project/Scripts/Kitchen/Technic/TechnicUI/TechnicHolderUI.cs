using UnityEngine;
using UnityEngine.UI;
using MadnessCooking.General;

namespace MadnessCooking.Kitchen {
    public class TechnicHolderUI : MonoBehaviour {
        [SerializeField] private TechnicHolder _technicHolder;
        [SerializeField] private GameObject _panel;
        [SerializeField] private Image _icon;
        [SerializeField] private Sprite _repairIcon;
        [SerializeField] private CircleSlider _progressBar;

        private void Awake() {
            _technicHolder.CookChanged += UpdateWorkState;
            _technicHolder.RepairChanged += UpdateWorkState;
        }

        private void UpdateWorkState() {
            if (_technicHolder.Data.IsCooking || _technicHolder.Data.IsRepairing)
                StartWork();
            else
                StopWork();
        }

        private void Update() {
            if (!_technicHolder.Data.IsCooking && !_technicHolder.Data.IsRepairing)
                return;

            _progressBar.SetValue(_technicHolder.Data.NowWaitTime);
        }

        public void StartWork() {
            _progressBar.SetMaxValue(_technicHolder.Data.NeedWaitTime);
            if (_technicHolder.Data.IsCooking)
                _icon.sprite = _technicHolder.Data.NowOrder.Food.Icon;
            else if (_technicHolder.Data.IsRepairing)
                _icon.sprite = _repairIcon;

            ChangeState(true);
        }

        public void StopWork() {
            ChangeState(false);
        }

        private void ChangeState(bool newState) {
            _panel.SetActive(newState);
        }
    }
}
