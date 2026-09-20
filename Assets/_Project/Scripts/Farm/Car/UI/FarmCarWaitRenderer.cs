using TMPro;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    public class FarmCarWaitRenderer : MonoBehaviour {
        [SerializeField] private FarmCarWaitManager _manager;
        [SerializeField] private LocalizedText _infoText;
        [SerializeField] private string _sentMessage;
        [SerializeField] private string _returnsMessage;
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private GameObject _showSendedItemsButton;

        private void Awake() {
            _manager.StateChanged += UpdateState;
        }

        private void UpdateState(CarState newState) {
            _showSendedItemsButton.SetActive(newState == CarState.Sent);

            if (newState == CarState.Sent)
                _infoText.SetText(_sentMessage);
            else if (newState == CarState.Returns)
                _infoText.SetText(_returnsMessage);
        }

        private void Update() {
            if (_manager.CarState == CarState.Calm)
                return;

            UpdateText();
        }

        private void UpdateText() {
            int nowTime = (int)_manager.NowWaitTime;

            string munites = (nowTime / 60).ToString("00");
            string seconds = (nowTime % 60).ToString("00");
            _timeText.text = $"{munites}:{seconds}";
        }
    }
}
